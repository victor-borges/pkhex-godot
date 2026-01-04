using Godot;

namespace PKHeX.Godot.Scripts.CurrentPokemon;

public partial class NicknameLineEdit : LineEdit
{
    private SignalBus _signalBus = null!;
    private GameData _gameData = null!;

    public override void _Ready()
    {
        _gameData = GetNode<GameData>("/root/GameData");
        _signalBus = GetNode<SignalBus>("/root/SignalBus");

        _signalBus.CurrentPokemonChanged += CurrentPokemonChanged;
        _signalBus.FileLoaded += OnFileLoaded;
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
