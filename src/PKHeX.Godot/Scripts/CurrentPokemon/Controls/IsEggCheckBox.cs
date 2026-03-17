namespace PKHeX.Godot.Scripts.CurrentPokemon.Controls;

public partial class IsEggCheckBox : CheckBox
{
    public override void _Ready()
    {
        Application.Instance.CurrentPokemonChanged += CurrentPokemonChanged;

        Toggled += OnToggled;
    }

    private void OnToggled(bool toggledOn)
    {
        if (Application.CurrentPokemon is null)
            return;

        Application.CurrentPokemon.IsEgg = toggledOn;
        Application.Instance.EmitEventCurrentPokemonChanged();
    }

    private void CurrentPokemonChanged()
    {
        if (Application.SaveFile is null || Application.CurrentPokemon is null)
        {
            Disabled = true;
            SetPressedNoSignal(false);
        }
        else
        {
            Disabled = false;
            SetPressedNoSignal(Application.CurrentPokemon.IsEgg);
        }
    }
}
