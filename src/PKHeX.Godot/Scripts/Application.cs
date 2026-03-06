using System;
using System.IO;
using Godot;
using PKHeX.Core;
using PKHeX.Facade;
using PKHeX.Facade.Pokemons;

namespace PKHeX.Godot.Scripts;

public partial class Application : Node
{
    public Game? Game
    {
        get;
        private set
        {
            field = value;
            FileLoaded?.Invoke();
        }
    }

    public Pokemon? CurrentPokemon
    {
        get;
        set
        {
            field = value;
            CurrentPokemonChanged?.Invoke();
        }
    }

    private int CurrentBoxIndex
    {
        get;
        set
        {
            field = value;
            BoxChanged?.Invoke(value);
        }
    }

    public override void _Ready()
    {
        var lang = GameInfo.CurrentLanguage;
        LocalizeUtil.InitializeStrings(lang, FakeSaveFile.Default);
    }

    public void TriggerCurrentPokemonChanged()
    {
        CurrentPokemonChanged?.Invoke();
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

    public void LoadSave(string path)
    {
        Game = Game.LoadFrom(path);

        PartyChanged?.Invoke();
        CurrentBoxIndex = 0;
        CurrentPokemon = null;
    }

    public void LoadPCData(string path)
    {
        if (Game is null)
            return;

        var bytes = File.ReadAllBytes(path);
        var didSet = Game.SaveFile.SetPCBinary(bytes);

        if (!didSet)
            return;

        Game = new Game(Game.SaveFile);
        CurrentBoxIndex = 0;
    }

    public event Action? FileLoaded;
    public event Action? PartyChanged;
    public event Action<int>? BoxChanged;
    public event Action? CurrentPokemonChanged;
}
