namespace PKHeX.Godot.Scripts.CurrentPokemon.Controls;

public partial class PokerusDaysSpinBox : SpinBox
{
        private Label _pokerusDaysLabel = null!;

    public override void _Ready()
    {
        _pokerusDaysLabel = GetNode<Label>("../PokerusDaysLabel");
        Application.Instance.CurrentPokemonChanged += CurrentPokemonChanged;

        ValueChanged += OnValueChanged;
    }

    private void OnValueChanged(double value)
    {
        if (Application.CurrentPokemon is null)
            return;

        Application.CurrentPokemon.PokerusDays = (int)value;
        Application.Instance.EmitEventCurrentPokemonChanged();
    }

    private void CurrentPokemonChanged()
    {
        if (Application.SaveFile is null || Application.CurrentPokemon is null)
        {
            Editable = false;
            SetValueNoSignal(0);
        }
        else
        {
            var pkm = Application.CurrentPokemon;
            Editable = pkm.IsPokerusInfected;
            Visible = _pokerusDaysLabel.Visible = !pkm.IsPokerusCured;
            SetMax(Pokerus.GetMaxDuration(pkm.PokerusStrain));
            SetValueNoSignal(pkm.PokerusDays);
        }
    }
}
