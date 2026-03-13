namespace PKHeX.Godot.Scripts.CurrentPokemon.Controls;

public partial class PokerusDaysSpinBox : SpinBox
{
    private Application _application = null!;
    private Label _pokerusDaysLabel = null!;

    public override void _Ready()
    {
        _application = GetNode<Application>(Application.NodePath);
        _pokerusDaysLabel = GetNode<Label>("../PokerusDaysLabel");
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
            Visible = _pokerusDaysLabel.Visible = !pkm.IsPokerusCured;
            SetMax(Pokerus.GetMaxDuration(pkm.PokerusStrain));
            SetValueNoSignal(pkm.PokerusDays);
        }
    }
}
