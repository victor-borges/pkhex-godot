namespace PKHeX.Godot.Scripts.Party;

public partial class PartySlot : Slot
{
    [Export] public int SlotIndex { get; set; }

    public override void _Ready()
    {
        base._Ready();
        Application.Instance.PartyChanged += OnPartyChanged;
    }

    private void OnPartyChanged()
    {
        if (Application.SaveFile is null || SlotIndex >= 6)
            return;

        var pokemon = Application.SaveFile.GetPartySlotAtIndex(SlotIndex);
        SetPokemon(pokemon);
    }
}
