using System.IO;

namespace PKHeX.Godot.Scripts;

public partial class Application : Node
{
    public static readonly NodePath NodePath = "/root/Application";

    public SaveFile? Game { get; private set; }

    public PKM? CurrentPokemon
    {
        get;
        set
        {
            if (value is null)
            {
                field = null;
                return;
            }

            var isPokemonPresent = EntityDetection.GetFuncIsPresent(value);
            field = isPokemonPresent(value.Data) ? value : null;

            EmitSignalCurrentPokemonChanged();
        }
    }

    private int CurrentBoxIndex
    {
        get;
        set
        {
            field = value;
            EmitSignalBoxChanged(value);
        }
    }

    public override void _Ready()
    {
        var lang = GameInfo.CurrentLanguage;
        LocalizeUtil.InitializeStrings(lang, FakeSaveFile.Default);
    }

    public void EmitEventCurrentPokemonChanged() => EmitSignalCurrentPokemonChanged();

    public void GoToPreviousBox()
    {
        if (Game is null)
            return;

        var totalBoxes = Game.BoxesUnlocked == -1 ? Game.BoxCount : Game.BoxesUnlocked;
        var previousBoxIndex = (CurrentBoxIndex - 1 + totalBoxes) % totalBoxes;
        CurrentBoxIndex = previousBoxIndex;
    }

    public void GoToNextBox()
    {
        if (Game is null)
            return;

        var totalBoxes = Game.BoxesUnlocked == -1 ? Game.BoxCount : Game.BoxesUnlocked;
        var nextBoxIndex = (CurrentBoxIndex + 1) % totalBoxes;
        CurrentBoxIndex = nextBoxIndex;
    }

    public void LoadSave(string path)
    {
        Game = SaveUtil.GetSaveFile(path) ?? throw new FileNotFoundException(path);
        Game.Metadata.SetExtraInfo(path);

        // if (!SanityCheckSAV(ref sav))
        //     return true;

        if (Game is SAV3 sav3)
            EReaderBerrySettings.LoadFrom(sav3);

        ParseSettings.InitFromSaveFileData(Game); // physical GB, no longer used in logic
        RecentTrainerCache.SetRecentTrainer(Game);

        GameInfo.FilteredSources = new FilteredGameDataSource(Game, GameInfo.Sources);

        Game.State.Edited = false;

        EmitSignalFileLoaded();
        EmitSignalPartyChanged();
        CurrentBoxIndex = 0;
        CurrentPokemon = null;
    }

    public void LoadPCData(string path)
    {
        if (Game is null)
            return;

        var bytes = File.ReadAllBytes(path);
        var didSet = Game.SetPCBinary(bytes);

        if (!didSet)
            return;

        EmitSignalFileLoaded();
        CurrentBoxIndex = 0;
    }

    [Signal] public delegate void FileLoadedEventHandler();
    [Signal] public delegate void PartyChangedEventHandler();
    [Signal] public delegate void BoxChangedEventHandler(int boxIndex);
    [Signal] public delegate void CurrentPokemonChangedEventHandler();
}
