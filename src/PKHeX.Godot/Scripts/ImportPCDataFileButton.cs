using System.IO;
using Godot;
using PKHeX.Facade;

namespace PKHeX.Godot.Scripts;

public partial class ImportPCDataFileButton : Button
{
    private SignalBus _signalBus = null!;
    private GameData _gameData = null!;
    private FileDialog _importPCDataFileDialog = null!;

    public override void _Ready()
    {
        _signalBus = GetNode<SignalBus>("/root/SignalBus");
        _gameData = GetNode<GameData>("/root/GameData");
        _importPCDataFileDialog = GetNode<FileDialog>("../ImportPCDataFileDialog");
    }

    private void OnButtonPressed()
    {
        _importPCDataFileDialog.Show();
    }

    private void OnImportPCDataFileDialogFileSelected(string path)
    {
        if (_gameData.Game is null)
            return;

        var bytes = File.ReadAllBytes(path);
        var didSet = _gameData.Game.SaveFile.SetPCBinary(bytes);

        if (!didSet)
            return;

        _gameData.Game = new Game(_gameData.Game.SaveFile);
        _signalBus.EmitSignal(SignalBus.SignalName.BoxChanged, 0);
    }
}
