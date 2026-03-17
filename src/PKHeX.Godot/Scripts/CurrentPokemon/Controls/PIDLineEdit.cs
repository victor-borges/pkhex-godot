namespace PKHeX.Godot.Scripts.CurrentPokemon.Controls;

public partial class PIDLineEdit : LineEdit
{
    public override void _Ready()
    {
        Application.Instance.CurrentPokemonChanged += CurrentPokemonChanged;
    }

    private void CurrentPokemonChanged()
    {
        Text = Application.CurrentPokemon?.PID.ToString("X8") ?? string.Empty;
    }
}
