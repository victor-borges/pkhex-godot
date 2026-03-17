namespace PKHeX.Godot.Scripts.CurrentPokemon.Controls;

public partial class PokerusStrainSpinBox : SpinBox
{
    public override void _Ready()
    {
        Application.Instance.CurrentPokemonChanged += CurrentPokemonChanged;

        ValueChanged += OnValueChanged;
    }

    private void OnValueChanged(double value)
    {
        if (Application.CurrentPokemon is null)
            return;

        Application.CurrentPokemon.PokerusStrain = (int)value;
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
            Editable = Application.CurrentPokemon.IsPokerusInfected;
            SetValueNoSignal(Application.CurrentPokemon.PokerusStrain);
        }
    }
}
