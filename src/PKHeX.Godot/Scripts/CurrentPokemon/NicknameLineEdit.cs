using Godot;

namespace PKHeX.Godot.Scripts.CurrentPokemon;

public partial class NicknameLineEdit : LineEdit
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
        if (_gameData.Game is null || _gameData.CurrentPokemon is null)
        {
            Text = string.Empty;
        }
        else
        {
            var isNicknamed = _gameData.CurrentPokemon.Pkm.IsNicknamed;
            Text = isNicknamed ? _gameData.CurrentPokemon.Nickname : string.Empty;
        }
    }

    private void OnFileLoaded(string _)
    {
        Text = string.Empty;
    }
}
