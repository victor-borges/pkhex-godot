using Godot;
using PKHeX.Facade;

namespace PKHeX.Godot.Scripts;

public partial class OpenFileButton : Button
{
	private Application _application = null!;
	private FileDialog _openFileDialog = null!;
	private Label _gameLabel = null!;

	public override void _Ready()
	{
		_application = GetNode<Application>("/root/Application");
		_openFileDialog = GetNode<FileDialog>("%OpenFileDialog");
		_gameLabel = GetNode<Label>("%GameLabel");
	}

	private void OnButtonPressed()
	{
		_openFileDialog.Show();
	}

	private void OnOpenFileDialogFileSelected(string path)
	{
		_application.LoadSave(path);
		_gameLabel.Text = $"Game: {_application.Game?.GameVersionApproximation.Name}";
	}
}
