namespace PKHeX.Godot.Scripts.Box;

public partial class BoxSlot : Slot
{
    [Export] public int SlotIndex { get; set; }

    public override void _Ready()
    {
        base._Ready();
        Application.BoxChanged += OnBoxChanged;
        Application.FileLoaded += OnFileLoaded;
    }

    private void OnBoxChanged(int boxIndex)
    {
        if (Application.Game is null)
        {
            SetPokemon(null);
        }
        else
        {
            var pokemon = Application.Game.GetBoxSlotAtIndex(boxIndex, SlotIndex);
            SetPokemon(pokemon);
        }
    }

    private void OnFileLoaded()
    {
        Visible = Application.Game is not null && SlotIndex < Application.Game.BoxSlotCount;
    }
}
