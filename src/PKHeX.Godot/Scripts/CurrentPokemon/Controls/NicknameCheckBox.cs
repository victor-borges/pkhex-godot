namespace PKHeX.Godot.Scripts.CurrentPokemon.Controls;

public partial class NicknameCheckBox : CheckBox
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

        if (toggledOn)
        {
            Application.CurrentPokemon.IsNicknamed = true;
        }
        else
        {
            Application.CurrentPokemon.ClearNickname();
        }

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
            var isNicknamed = Application.CurrentPokemon.IsNicknamed;
            SetPressedNoSignal(isNicknamed);
        }
    }
}
