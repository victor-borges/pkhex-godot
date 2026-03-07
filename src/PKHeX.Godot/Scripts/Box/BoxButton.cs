namespace PKHeX.Godot.Scripts.Box;

public partial class BoxButton : Button
{
    private Application _application = null!;

    public override void _Ready()
    {
        _application = GetNode<Application>(Application.NodePath);
        _application.BoxChanged += OnBoxChanged;
    }

    private void OnBoxChanged(int boxIndex)
    {
        if (_application.Game is null)
            return;

        Text = _application.Game.SaveFile.Version is GameVersion.PLA
            ? $"Pasture {boxIndex + 1}"
            : $"Box {boxIndex + 1}";
    }
}
