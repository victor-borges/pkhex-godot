namespace PKHeX.Godot.Scripts.Party;

public partial class PartySlot : Slot
{
    [Export] public int SlotIndex { get; set; }

    public override void _Ready()
    {
        base._Ready();
        Application.PartyChanged += OnPartyChanged;
    }

    private void OnPartyChanged()
    {
        if (Application.Game is null || SlotIndex >= 6)
            return;

        var pokemon = Application.Game.GetPartySlotAtIndex(SlotIndex);
        SetPokemon(pokemon);
    }
}
