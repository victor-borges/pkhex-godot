using Godot;

namespace PKHeX.Godot.Scripts.CurrentPokemon;

public partial class PIDLineEdit : LineEdit
{
    private GameData _gameData = null!;

    public override void _Ready()
    {
        _gameData = GetNode<GameData>("/root/GameData");

        _gameData.CurrentPokemonChanged += CurrentPokemonChanged;
        _gameData.FileLoaded += OnFileLoaded;
    }

    private void CurrentPokemonChanged()
    {
        Text = _gameData.CurrentPokemon?.PID.ToString("X8") ?? string.Empty;
    }

    private void OnFileLoaded(string _)
    {
        Text = string.Empty;
    }
}
