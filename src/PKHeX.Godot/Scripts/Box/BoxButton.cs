namespace PKHeX.Godot.Scripts.Box;

public partial class BoxButton : Button
{
    public override void _Ready()
    {
        Application.Instance.BoxChanged += OnBoxChanged;
    }

    private void OnBoxChanged(int boxIndex)
    {
        if (Application.SaveFile is null)
            return;

        Text = Application.SaveFile.Version is GameVersion.PLA
            ? $"Pasture {boxIndex + 1}"
            : $"Box {boxIndex + 1}";
    }
}
