using Godot;

namespace PKHeX.Godot.Scripts.Party;

public partial class PartySlot : Slot
{
    [Export] public int SlotIndex { get; set; }

    public override void _Ready()
    {
        base._Ready();
        Pressed += OnButtonPressed;
        Application.PartyChanged += OnPartyChanged;
    }

    private void OnPartyChanged()
    {
        if (SlotIndex >= Application.Game?.Trainer.Party.Pokemons.Count)
            return;

        var pokemon = Application.Game?.Trainer.Party.Pokemons[SlotIndex];
        SetPokemon(pokemon);
    }

    private void OnButtonPressed()
    {
        Application.CurrentPokemon = Pokemon?.Clone();
    }
}
