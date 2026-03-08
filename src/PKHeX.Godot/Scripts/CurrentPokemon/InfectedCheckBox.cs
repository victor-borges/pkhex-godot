namespace PKHeX.Godot.Scripts.CurrentPokemon;

public partial class InfectedCheckBox : CheckBox
{
    private Application _application = null!;

    public override void _Ready()
    {
        _application = GetNode<Application>(Application.NodePath);
        _application.CurrentPokemonChanged += CurrentPokemonChanged;

        Toggled += OnToggled;
    }

    private void OnToggled(bool toggledOn)
    {
        _application.CurrentPokemon?.Pkm.IsPokerusInfected = toggledOn;
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
            SetPressedNoSignal(_application.CurrentPokemon.Pkm.IsPokerusInfected);
        }
    }
}
