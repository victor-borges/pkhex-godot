namespace PKHeX.Godot.Scripts.CurrentPokemon.Controls;

public partial class EXPSpinBox : SpinBox
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

        Application.CurrentPokemon.EXP = (uint)value;
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
            Editable = true;
            var pkm = Application.CurrentPokemon;

            SetMax(Experience.GetEXP(Experience.MaxLevel, pkm.PersonalInfo.EXPGrowth));
            SetMin(Experience.GetEXP(Experience.MinLevel, pkm.PersonalInfo.EXPGrowth));
            SetValueNoSignal(Application.CurrentPokemon.EXP);
        }
    }
}
