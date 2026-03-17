namespace PKHeX.Godot.Scripts;

public partial class ImportPCDataFileButton : Button
{
    private FileDialog _importPCDataFileDialog = null!;

    public override void _Ready()
    {
        _importPCDataFileDialog = GetNode<FileDialog>("%ImportPCDataFileDialog");
    }

    private void OnButtonPressed()
    {
        _importPCDataFileDialog.Show();
    }

    private void OnImportPCDataFileDialogFileSelected(string path)
    {
        if (Application.SaveFile is null)
            return;

        Application.Instance.LoadPCData(path);
    }
}
