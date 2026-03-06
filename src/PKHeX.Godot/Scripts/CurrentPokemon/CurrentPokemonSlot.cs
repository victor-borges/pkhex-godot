namespace PKHeX.Godot.Scripts.CurrentPokemon;

public partial class CurrentPokemonSlot : Slot
{
    public override void _Ready()
    {
        base._Ready();
        GameData.CurrentPokemonChanged += () => SetPokemon(GameData.CurrentPokemon);
    }
}
