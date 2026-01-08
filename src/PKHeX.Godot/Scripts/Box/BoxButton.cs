using Godot;

namespace PKHeX.Godot.Scripts.Box;

public partial class BoxButton : Button
{
    private SignalBus _signalBus = null!;

    public override void _Ready()
    {
        _signalBus = GetNode<SignalBus>("/root/SignalBus");
        _signalBus.BoxChanged += OnBoxChanged;
    }

    private void OnBoxChanged(int boxIndex)
    {
        Text = $"Box {boxIndex + 1}";
    }
}
