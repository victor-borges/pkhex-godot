using Godot;

namespace PKHeX.Godot.Scripts.Box;

public partial class BoxSlot : Slot
{
    [Export] public int SlotIndex { get; set; }

    public override void _Ready()
    {
        base._Ready();
        Pressed += OnButtonPressed;
        GameData.BoxChanged += OnBoxChanged;
    }

    private void OnBoxChanged(int boxIndex)
    {
        var index = (boxIndex * 30) + SlotIndex;
        var pokemon = GameData.Game?.Trainer.PokemonBox.All[index];
        SetPokemon(pokemon);
    }

    private void OnButtonPressed()
    {
        GameData.CurrentPokemon = Pokemon?.Clone();
    }
}
