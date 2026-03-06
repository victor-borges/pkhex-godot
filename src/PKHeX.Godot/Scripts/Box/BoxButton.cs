using Godot;

namespace PKHeX.Godot.Scripts.Box;

public partial class BoxButton : Button
{
    private Application _application = null!;

    public override void _Ready()
    {
        _application = GetNode<Application>("/root/Application");
        _application.BoxChanged += OnBoxChanged;
    }

    private void OnBoxChanged(int boxIndex)
    {
        Text = $"Box {boxIndex + 1}";
    }
}
