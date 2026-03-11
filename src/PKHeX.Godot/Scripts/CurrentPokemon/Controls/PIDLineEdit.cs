namespace PKHeX.Godot.Scripts.CurrentPokemon.Controls;

public partial class PIDLineEdit : LineEdit
{
    private Application _application = null!;

    public override void _Ready()
    {
        _application = GetNode<Application>(Application.NodePath);
        _application.CurrentPokemonChanged += CurrentPokemonChanged;
    }

    private void CurrentPokemonChanged()
    {
        Text = _application.CurrentPokemon?.PID.ToString("X8") ?? string.Empty;
    }
}
