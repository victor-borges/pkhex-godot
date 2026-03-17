namespace PKHeX.Godot.Scripts.Box;

public partial class PreviousBoxButton : Button
{
    public override void _Ready()
    {
        Pressed += OnButtonPressed;
    }

    private void OnButtonPressed()
    {
        Application.Instance.GoToPreviousBox();
    }
}
