namespace PKHeX.Godot.Scripts;

public partial class ImportPCDataFileButton : Button
{
    private Application _application = null!;
    private FileDialog _importPCDataFileDialog = null!;

    public override void _Ready()
    {
        _application = GetNode<Application>(Application.NodePath);
        _importPCDataFileDialog = GetNode<FileDialog>("%ImportPCDataFileDialog");
    }

    private void OnButtonPressed()
    {
        _importPCDataFileDialog.Show();
    }

    private void OnImportPCDataFileDialogFileSelected(string path)
    {
        if (_application.Game is null)
            return;

        _application.LoadPCData(path);
    }
}
