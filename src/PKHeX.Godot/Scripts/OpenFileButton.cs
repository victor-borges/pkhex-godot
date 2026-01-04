using System.IO;
using Godot;
using PKHeX.Facade;

namespace PKHeX.Godot.Scripts;

public partial class OpenFileButton : Button
{
	private SignalBus _signalBus = null!;
	private FileDialog _openFileDialog = null!;
	private Label _gameLabel = null!;

	public override void _Ready()
	{
		_signalBus = GetNode<SignalBus>("/root/SignalBus");
		_openFileDialog = GetNode<FileDialog>("../OpenFileDialog");
		_gameLabel = GetNode<Label>("../GameLabel");
	}

	private void OnButtonPressed()
	{
		_openFileDialog.Show();
	}

	private void OnOpenFileDialogFileSelected(string path)
	{
		var game = Game.LoadFrom(path);
		_gameLabel.Text = $"Game: {game.GameVersionApproximation.Name}";

		_signalBus.EmitSignal(SignalBus.SignalName.FileLoaded, path);
	}
}
