using Godot;

namespace PKHeX.Godot.Scripts.CurrentPokemon;

public partial class EXPSpinBox : SpinBox
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
            Value = 0;
        }
        else
        {
            Value = _gameData.CurrentPokemon.Experience;
        }
    }

    private void OnFileLoaded(string _)
    {
        Value = 0;
    }
}
