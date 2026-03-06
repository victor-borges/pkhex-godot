using Godot;

namespace PKHeX.Godot.Scripts.CurrentPokemon;

public partial class InfectedCheckBox : CheckBox
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
        _application.CurrentPokemon?.Pkm.IsPokerusInfected = pressed;
        _application.TriggerCurrentPokemonChanged();
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
