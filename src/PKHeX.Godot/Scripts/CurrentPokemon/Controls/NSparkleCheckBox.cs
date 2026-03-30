namespace PKHeX.Godot.Scripts.CurrentPokemon.Controls;

public partial class NSparkleCheckBox : CheckBox
{
    public override void _Ready()
    {
        Application.Instance.CurrentPokemonChanged += CurrentPokemonChanged;

        Toggled += OnButtonPressed;
    }

    private void OnButtonPressed(bool pressed)
    {
        if (Application.CurrentPokemon is not PK5 pk5)
            return;

        pk5.NSparkle = pressed;
        Application.Instance.EmitEventCurrentPokemonChanged();
    }

    private void CurrentPokemonChanged()
    {
        if (Application.SaveFile is null || Application.CurrentPokemon is not PK5 pk5)
        {
            Disabled = true;
            SetPressedNoSignal(false);
        }
        else
        {
            Disabled = false;
            SetPressedNoSignal(pk5.NSparkle);
        }
    }
}
