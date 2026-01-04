using Godot;

namespace PKHeX.Godot.Scripts;

public partial class Box : GridContainer
{
    private SignalBus _signalBus = null!;
    private GameData _gameData = null!;
    private Button _boxButton = null!;

    private Slot[] _boxSlots = new Slot[30];

    public override void _Ready()
    {
        _gameData = GetNode<GameData>("/root/GameData");
        _signalBus = GetNode<SignalBus>("/root/SignalBus");
        _boxButton = GetNode<Button>("%BoxButton");

        for (int i = 0; i < 30; i++)
            _boxSlots[i] = GetNode<Slot>($"%BoxSlot{i}");

        _signalBus.BoxChanged += OnBoxChanged;
    }

    private void OnBoxChanged(int boxIndex)
    {
        _boxButton.Text = $"Box {boxIndex + 1}";

        for (int i = 0; i < 30; i++)
        {
            var index = (boxIndex * 30) + i;
            var pokemon = _gameData.Game?.Trainer.PokemonBox.All[index];
            _boxSlots[i].LoadSprites(pokemon);
        }
    }
}
