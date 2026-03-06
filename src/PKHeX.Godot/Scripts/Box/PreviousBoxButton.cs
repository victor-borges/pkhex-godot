using Godot;

namespace PKHeX.Godot.Scripts.Box;

public partial class PreviousBoxButton : Button
{
    private Application _application = null!;

    public override void _Ready()
    {
        _application = GetNode<Application>(Application.NodePath);
    }

    private void OnButtonPressed()
    {
        _application.GoToPreviousBox();
    }
}
