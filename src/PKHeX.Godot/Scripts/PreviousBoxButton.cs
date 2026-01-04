using Godot;

namespace PKHeX.Godot.Scripts;

public partial class PreviousBoxButton : Button
{
    private GameData _gameData = null!;

    public override void _Ready()
    {
        _gameData = GetNode<GameData>("/root/GameData");
    }

    private void OnButtonPressed()
    {
        _gameData.GoToPreviousBox();
    }
}
