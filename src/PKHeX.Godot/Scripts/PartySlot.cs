using Godot;

namespace PKHeX.Godot.Scripts;

public partial class PartySlot : Slot
{
    [Export] public int SlotIndex { get; set; }

    public override void _Ready()
    {
        base._Ready();
        Pressed += OnButtonPressed;
        SignalBus.PartyChanged += OnPartyChanged;
    }

    private void OnPartyChanged()
    {
        if (SlotIndex >= GameData.Game?.Trainer.Party.Pokemons.Count)
            return;

        var pokemon = GameData.Game?.Trainer.Party.Pokemons[SlotIndex];
        SetPokemon(pokemon);
    }

    private void OnButtonPressed()
    {
        SignalBus.EmitSignal(SignalBus.SignalName.PartyPokemonSelected, SlotIndex);
    }
}
