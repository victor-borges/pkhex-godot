using Godot;
using PKHeX.Core;

namespace PKHeX.Godot.Scripts;

public partial class Box : GridContainer
{
    private SignalBus _signalBus;
    private GameData _gameData;
    private Button _boxButton;

    private Slot[] _boxSlots = new Slot[30];
    private PKM[] _boxPkm = new PKM[30];

    public override void _Ready()
    {
        _gameData = GetNode<GameData>("/root/GameData");
        _signalBus = GetNode<SignalBus>("/root/SignalBus");
        _boxButton = GetNode<Button>("%BoxButton");

        for (int i = 1; i <= 30; i++)
            _boxSlots[i - 1] = GetNode<Slot>($"%BoxSlot{i}");

        _signalBus.BoxChanged += OnBoxChanged;
    }

    private void OnBoxChanged(int boxIndex)
    {
        _boxButton.Text = $"Box {boxIndex + 1}";

        for (int i = 0; i < 30; i++)
        {
            var pkm = _gameData.Game.SaveFile.GetBoxSlotAtIndex(boxIndex, i);
            _boxPkm[i] = pkm;
            _boxSlots[i].LoadSprites(pkm);
        }
    }
}
