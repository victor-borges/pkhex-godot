namespace PKHeX.Godot.Scripts.CurrentPokemon.Controls;

public partial class PokerusCuredCheckBox : CheckBox
{
    private Application _application = null!;

    public override void _Ready()
    {
        _application = GetNode<Application>(Application.NodePath);
        _application.CurrentPokemonChanged += CurrentPokemonChanged;

        Toggled += OnButtonPressed;
    }

    private void OnButtonPressed(bool pressed)
    {
        if (_application.CurrentPokemon is null)
            return;

        _application.CurrentPokemon.IsPokerusCured = pressed;
        _application.EmitEventCurrentPokemonChanged();
    }

    private void CurrentPokemonChanged()
    {
        if (_application.Game is null || _application.CurrentPokemon is null)
        {
            Disabled = true;
            SetPressedNoSignal(false);
        }
        else
        {
            Disabled = false;
            SetPressedNoSignal(_application.CurrentPokemon.IsPokerusCured);
        }
    }
}
