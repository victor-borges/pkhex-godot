namespace PKHeX.Godot.Scripts.CurrentPokemon;

public partial class CurrentPokemonSlot : Slot
{
    public override void _Ready()
    {
        base._Ready();
        SignalBus.CurrentPokemonChanged += () => SetPokemon(GameData.CurrentPokemon);
    }
}
