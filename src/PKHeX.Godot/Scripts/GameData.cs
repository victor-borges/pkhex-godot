using Godot;
using PKHeX.Facade;
using PKHeX.Facade.Pokemons;

namespace PKHeX.Godot.Scripts;

public partial class GameData : Node
{
    private SignalBus _signalBus = null!;
    public Game? Game { get; private set; }
    public Pokemon? CurrentPokemon { get; private set; }

    public int CurrentBoxIndex
    {
        get;
        private set
        {
            field = value;
            _signalBus.EmitSignal(SignalBus.SignalName.BoxChanged, value);
        }
    }

    public override void _Ready()
    {
        _signalBus = GetNode<SignalBus>("/root/SignalBus");
        _signalBus.FileLoading += OnFileLoading;
        _signalBus.BoxPokemonSelected += OnBoxPokemonSelected;
    }

    private void OnFileLoading(string path)
    {
        Game = Game.LoadFrom(path);
        CurrentBoxIndex = 0;

        _signalBus.EmitSignal(SignalBus.SignalName.FileLoaded, path);
    }

    private void OnBoxPokemonSelected(int slotIndex)
    {
        if (Game is null)
            return;

        var index = (CurrentBoxIndex * 30) + slotIndex;
        CurrentPokemon = Game?.Trainer.PokemonBox.All[index];

        _signalBus.EmitSignal(SignalBus.SignalName.CurrentPokemonChanged);
    }

    public void GoToPreviousBox()
    {
        if (Game is null)
            return;

        var totalBoxes = Game.SaveFile.BoxesUnlocked;
        var previousBoxIndex = (CurrentBoxIndex - 1 + totalBoxes) % totalBoxes;
        CurrentBoxIndex = previousBoxIndex;
    }

    public void GoToNextBox()
    {
        if (Game is null)
            return;

        var totalBoxes = Game.SaveFile.BoxesUnlocked;
        var nextBoxIndex = (CurrentBoxIndex + 1) % totalBoxes;
        CurrentBoxIndex = nextBoxIndex;
    }
}
