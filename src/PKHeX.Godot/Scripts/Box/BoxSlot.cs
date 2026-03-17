namespace PKHeX.Godot.Scripts.Box;

public partial class BoxSlot : Slot
{
    [Export] public int SlotIndex { get; set; }

    public override void _Ready()
    {
        base._Ready();
        Application.Instance.BoxChanged += OnBoxChanged;
        Application.Instance.FileLoaded += OnFileLoaded;
    }

    private void OnBoxChanged(int boxIndex)
    {
        if (Application.SaveFile is null)
        {
            SetPokemon(null);
        }
        else
        {
            var pokemon = Application.SaveFile.GetBoxSlotAtIndex(boxIndex, SlotIndex);
            SetPokemon(pokemon);
        }
    }

    private void OnFileLoaded()
    {
        Visible = Application.SaveFile is not null && SlotIndex < Application.SaveFile.BoxSlotCount;
    }
}
