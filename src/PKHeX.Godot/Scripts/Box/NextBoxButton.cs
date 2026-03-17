namespace PKHeX.Godot.Scripts.Box;

public partial class NextBoxButton : Button
{
    public override void _Ready()
    {
        Pressed += OnButtonPressed;
    }

    private void OnButtonPressed()
    {
        Application.Instance.GoToNextBox();
    }
}
