namespace PKHeX.Godot.Scripts.CurrentPokemon;

public partial class PokerusDaysSpinBox : SpinBox
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
        _application.CurrentPokemon?.Pkm.PokerusDays = (int)value;
        _application.TriggerCurrentPokemonChanged();
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
            var pkm = _application.CurrentPokemon.Pkm;

            Editable = pkm.IsPokerusInfected;
            SetMax(Pokerus.GetMaxDuration(pkm.PokerusStrain));
            SetValueNoSignal(_application.CurrentPokemon.Pkm.PokerusDays);
        }
    }
}
