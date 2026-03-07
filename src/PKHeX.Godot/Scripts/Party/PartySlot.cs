namespace PKHeX.Godot.Scripts.Party;

public partial class PartySlot : Slot
{
    [Export] public int SlotIndex { get; set; }

    public override void _Ready()
    {
        base._Ready();
        Application.PartyChanged += OnPartyChanged;

        Pressed += OnButtonPressed;
    }

    private void OnButtonPressed()
    {
        Application.CurrentPokemon = Pokemon?.Clone();
    }

    private void OnPartyChanged()
    {
        if (SlotIndex >= Application.Game?.Trainer.Party.Pokemons.Count)
            return;

        var pokemon = Application.Game?.Trainer.Party.Pokemons[SlotIndex];
        SetPokemon(pokemon);
    }
}
