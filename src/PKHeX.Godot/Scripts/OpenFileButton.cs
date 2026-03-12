namespace PKHeX.Godot.Scripts;

public partial class OpenFileButton : Button
{
	private Application _application = null!;
	private FileDialog _openFileDialog = null!;
	private Label _gameLabel = null!;

	public override void _Ready()
	{
		_application = GetNode<Application>(Application.NodePath);
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

		if (_application.Game is null)
			return;

		var version = _application.Game.Version;
		var versionText = GameInfo.Sources.VersionDataSource.FirstOrDefault(s => s.Value == (int)version)?.Text ?? version.ToString();
		_gameLabel.Text = $"Game: {versionText}";
	}
}
