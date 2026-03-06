using Godot;

namespace PKHeX.Godot.Scripts.CurrentPokemon;

public partial class RerollButton : Button
{
    private GameData _gameData = null!;

    public override void _Ready()
    {
        _gameData = GetNode<GameData>("/root/GameData");

        Pressed += OnButtonPressed;
    }

    private void OnButtonPressed()
    {
        _gameData.CurrentPokemon?.RerollPID();
        _gameData.TriggerCurrentPokemonChanged();
    }
}
