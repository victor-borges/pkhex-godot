using Godot;

namespace PKHeX.Godot.Scripts.CurrentPokemon;

public partial class RerollButton : Button
{
    private SignalBus _signalBus = null!;
    private GameData _gameData = null!;

    public override void _Ready()
    {
        _gameData = GetNode<GameData>("/root/GameData");
        _signalBus = GetNode<SignalBus>("/root/SignalBus");

        Pressed += OnButtonPressed;
    }

    private void OnButtonPressed()
    {
        _gameData.CurrentPokemon?.RerollPID();
        _signalBus.EmitSignal(SignalBus.SignalName.CurrentPokemonChanged);
    }
}
