namespace PKHeX.Godot.Scripts.CurrentPokemon.Controls;

public partial class PokerusCuredCheckBox : CheckBox
{
    public override void _Ready()
    {
        Application.Instance.CurrentPokemonChanged += CurrentPokemonChanged;

        Toggled += OnButtonPressed;
    }

    private void OnButtonPressed(bool pressed)
    {
        if (Application.CurrentPokemon is null)
            return;

        Application.CurrentPokemon.IsPokerusCured = pressed;
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
            SetPressedNoSignal(Application.CurrentPokemon.IsPokerusCured);
        }
    }
}
