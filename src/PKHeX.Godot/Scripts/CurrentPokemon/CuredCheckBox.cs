using Godot;

namespace PKHeX.Godot.Scripts.CurrentPokemon;

public partial class CuredCheckBox : CheckBox
{
    private Application _application = null!;

    public override void _Ready()
    {
        _application = GetNode<Application>(Application.NodePath);
        _application.CurrentPokemonChanged += CurrentPokemonChanged;
        _application.FileLoaded += OnFileLoaded;
    }

    private void OnButtonPressed(bool pressed)
    {
        _application.CurrentPokemon?.Pkm.IsPokerusCured = pressed;
        _application.TriggerCurrentPokemonChanged();
    }

    private void CurrentPokemonChanged()
    {
        if (_application.Game is null || _application.CurrentPokemon is null)
        {
            SetPressedNoSignal(false);
        }
        else
        {
            SetPressedNoSignal(_application.CurrentPokemon.Pkm.IsPokerusCured);
        }
    }

    private void OnFileLoaded()
    {
        SetPressedNoSignal(false);
    }
}
