namespace PKHeX.Godot.Scripts;

public partial class OpenFileButton : Button
{
	private FileDialog _openFileDialog = null!;
	private Label _gameLabel = null!;

	public override void _Ready()
	{
		_openFileDialog = GetNode<FileDialog>("%OpenFileDialog");
		_gameLabel = GetNode<Label>("%GameLabel");
	}

	private void OnButtonPressed()
	{
		_openFileDialog.Show();
	}

	private void OnOpenFileDialogFileSelected(string path)
	{
		Application.Instance.LoadSave(path);

		if (Application.SaveFile is null)
			return;

		var version = Application.SaveFile.Version;
		var versionText = GameInfo.Sources.VersionDataSource.FirstOrDefault(s => s.Value == (int)version)?.Text ?? version.ToString();
		_gameLabel.Text = $"Game: {versionText}";
	}
}
