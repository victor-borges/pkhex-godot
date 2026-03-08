namespace PKHeX.Godot.Scripts.CurrentPokemon;

public partial class PokerusStrainSpinBox : SpinBox
{
    private Application _application = null!;

    public override void _Ready()
    {
        _application = GetNode<Application>(Application.NodePath);
        _application.CurrentPokemonChanged += CurrentPokemonChanged;

        ValueChanged += OnValueChanged;
    }

    private void OnValueChanged(double value)
    {
        _application.CurrentPokemon?.Pkm.PokerusStrain = (int)value;
        _application.EmitEventCurrentPokemonChanged();
    }

    private void CurrentPokemonChanged()
    {
        if (_application.Game is null || _application.CurrentPokemon is null)
        {
            Editable = false;
            SetValueNoSignal(0);
        }
        else
        {
            Editable = _application.CurrentPokemon.Pkm.IsPokerusInfected;
            SetValueNoSignal(_application.CurrentPokemon.Pkm.PokerusStrain);
        }
    }
}
