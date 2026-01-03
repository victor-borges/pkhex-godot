using Godot;
using PKHeX.Core;
using PKHeX.Facade;

namespace PKHeX.Godot.Scripts;

public partial class OpenFileButton : Button
{
	private FileDialog _openFileDialog;
	private Label _gameLabel;

	private Slot[] _boxSlots = new Slot[30];
	private PKM[] _boxPkm = new PKM[30];

	public override void _Ready()
	{
		_openFileDialog = GetNode<FileDialog>("../OpenFileDialog");
		_gameLabel = GetNode<Label>("../GameLabel");

		for (int i = 1; i <= 30; i++)
		{
			_boxSlots[i - 1] = GetNode<Slot>($"%BoxSlot{i}");
		}
	}

	public void OnButtonPressed()
	{
		_openFileDialog.Show();
	}

	public void OnOpenFileDialogFileSelected(string path)
	{
		GD.Print($"Selected file: {path}");
		var game = Game.LoadFrom(path);
		_gameLabel.Text = $"Game: {game.GameVersionApproximation.Name}";

		var currentBox = 0;

		for (int i = 0; i < 30; i++)
		{
			var pkm = game.SaveFile.GetBoxSlotAtIndex(currentBox, i);
			_boxSlots[i].LoadSprites(pkm);
		}
	}
}
