using Godot;

namespace PKHeX.Godot.Scripts.Box;

public partial class BoxButton : Button
{
    private GameData _gameData = null!;

    public override void _Ready()
    {
        _gameData = GetNode<GameData>("/root/GameData");
        _gameData.BoxChanged += OnBoxChanged;
    }

    private void OnBoxChanged(int boxIndex)
    {
        Text = $"Box {boxIndex + 1}";
    }
}
