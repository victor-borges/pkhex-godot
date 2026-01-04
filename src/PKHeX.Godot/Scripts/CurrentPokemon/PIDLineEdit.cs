using Godot;

namespace PKHeX.Godot.Scripts.CurrentPokemon;

public partial class PIDLineEdit : LineEdit
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
        Text = _gameData.CurrentPokemon?.PID.ToString("X8") ?? string.Empty;
    }

    private void OnFileLoaded(string _)
    {
        Text = string.Empty;
    }
}
