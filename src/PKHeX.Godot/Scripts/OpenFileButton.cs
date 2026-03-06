using Godot;
using PKHeX.Facade;

namespace PKHeX.Godot.Scripts;

public partial class OpenFileButton : Button
{
	private GameData _gameData = null!;
	private FileDialog _openFileDialog = null!;
	private Label _gameLabel = null!;

	public override void _Ready()
	{
		_gameData = GetNode<GameData>("/root/GameData");
		_openFileDialog = GetNode<FileDialog>("%OpenFileDialog");
		_gameLabel = GetNode<Label>("%GameLabel");
	}

	private void OnButtonPressed()
	{
		_openFileDialog.Show();
	}

	private void OnOpenFileDialogFileSelected(string path)
	{
		_gameData.LoadSave(path);
		_gameLabel.Text = $"Game: {_gameData.Game?.GameVersionApproximation.Name}";
	}
}
