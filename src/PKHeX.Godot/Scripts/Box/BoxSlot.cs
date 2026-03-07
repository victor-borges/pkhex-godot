namespace PKHeX.Godot.Scripts.Box;

public partial class BoxSlot : Slot
{
    [Export] public int SlotIndex { get; set; }

    public override void _Ready()
    {
        base._Ready();
        Application.BoxChanged += OnBoxChanged;
        Application.FileLoaded += OnFileLoaded;

        Pressed += OnButtonPressed;
    }

    private void OnButtonPressed()
    {
        Application.CurrentPokemon = Pokemon?.Clone();
    }

    private void OnBoxChanged(int boxIndex)
    {
        if (Application.Game is null)
        {
            SetPokemon(null);
        }
        else
        {
            var index = (boxIndex * Application.Game.SaveFile.BoxSlotCount) + SlotIndex;
            var pokemon = Application.Game.Trainer.PokemonBox.All[index];
            SetPokemon(pokemon);
        }
    }

    private void OnFileLoaded()
    {
        Visible = Application.Game is not null && SlotIndex < Application.Game.SaveFile.BoxSlotCount;
    }
}
