using Godot;
using PKHeX.Facade;

namespace PKHeX.Godot.Scripts;

public partial class GameData : Node
{
    private SignalBus _signalBus;
    public Game Game { get; private set; }

    public int CurrentBoxIndex
    {
        get;
        private set
        {
            field = value;
            _signalBus.EmitSignal(SignalBus.SignalName.BoxChanged, value);
        }
    }

    public override void _Ready()
    {
        _signalBus = GetNode<SignalBus>("/root/SignalBus");
        _signalBus.FileLoaded += OnFileLoaded;
    }

    private void OnFileLoaded(string path)
    {
        Game = Game.LoadFrom(path);
        CurrentBoxIndex = 0;
    }

    public void GoToPreviousBox()
    {
        var totalBoxes = Game.SaveFile.BoxesUnlocked;
        var previousBoxIndex = (CurrentBoxIndex - 1 + totalBoxes) % totalBoxes;
        CurrentBoxIndex = previousBoxIndex;
    }

    public void GoToNextBox()
    {
        var totalBoxes = Game.SaveFile.BoxesUnlocked;
        var nextBoxIndex = (CurrentBoxIndex + 1) % totalBoxes;
        CurrentBoxIndex = nextBoxIndex;
    }
}
