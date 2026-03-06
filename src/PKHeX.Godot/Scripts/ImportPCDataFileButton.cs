using System.IO;
using Godot;
using PKHeX.Facade;

namespace PKHeX.Godot.Scripts;

public partial class ImportPCDataFileButton : Button
{
    private GameData _gameData = null!;
    private FileDialog _importPCDataFileDialog = null!;

    public override void _Ready()
    {
        _gameData = GetNode<GameData>("/root/GameData");
        _importPCDataFileDialog = GetNode<FileDialog>("%ImportPCDataFileDialog");
    }

    private void OnButtonPressed()
    {
        _importPCDataFileDialog.Show();
    }

    private void OnImportPCDataFileDialogFileSelected(string path)
    {
        if (_gameData.Game is null)
            return;

        _gameData.LoadPCData(path);
    }
}
