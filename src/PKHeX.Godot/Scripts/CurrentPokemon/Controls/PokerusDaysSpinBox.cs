namespace PKHeX.Godot.Scripts.CurrentPokemon.Controls;

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
        if (_application.CurrentPokemon is null)
            return;

        _application.CurrentPokemon.PokerusDays = (int)value;
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
            var pkm = _application.CurrentPokemon;

            Editable = pkm.IsPokerusInfected;
            SetMax(Pokerus.GetMaxDuration(pkm.PokerusStrain));
            SetValueNoSignal(pkm.PokerusDays);
        }
    }
}
