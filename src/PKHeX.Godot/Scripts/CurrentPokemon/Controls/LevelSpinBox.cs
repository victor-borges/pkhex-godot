namespace PKHeX.Godot.Scripts.CurrentPokemon.Controls;

public partial class LevelSpinBox : SpinBox
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

        Application.CurrentPokemon.CurrentLevel = (byte)value;
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
            SetValueNoSignal(Application.CurrentPokemon.CurrentLevel);
        }
    }
}
