using Godot;

namespace PKHeX.Godot.Scripts.CurrentPokemon;

public partial class PokerusStrainSpinBox : SpinBox
{
    private Application _application = null!;

    public override void _Ready()
    {
        _application = GetNode<Application>(Application.NodePath);
        _application.CurrentPokemonChanged += CurrentPokemonChanged;
        _application.FileLoaded += OnFileLoaded;

        ValueChanged += OnValueChanged;
    }

    private void OnValueChanged(double value)
    {
        _application.CurrentPokemon?.Pkm.PokerusStrain = (int)value;
        _application.TriggerCurrentPokemonChanged();
    }

    private void CurrentPokemonChanged()
    {
        if (_application.Game is null || _application.CurrentPokemon is null)
        {
            SetValueNoSignal(0);
        }
        else
        {
            SetValueNoSignal(_application.CurrentPokemon.Pkm.PokerusStrain);
        }
    }

    private void OnFileLoaded()
    {
        SetValueNoSignal(0);
    }
}
