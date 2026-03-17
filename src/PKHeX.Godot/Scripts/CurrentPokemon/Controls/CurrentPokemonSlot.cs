namespace PKHeX.Godot.Scripts.CurrentPokemon.Controls;

public partial class CurrentPokemonSlot : Slot
{
    public override void _Ready()
    {
        base._Ready();
        Application.Instance.CurrentPokemonChanged += () => SetPokemon(Application.CurrentPokemon);
    }
}
