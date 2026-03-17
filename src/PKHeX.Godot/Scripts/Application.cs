using System.IO;

namespace PKHeX.Godot.Scripts;

public partial class Application : Node
{
    public static Application Instance { get; private set; } = null!;
    public static SaveFile? SaveFile => Instance._saveFile;

    private SaveFile? _saveFile;
    private PKM? _currentPokemon;

    public static PKM? CurrentPokemon
    {
        get => Instance._currentPokemon;
        set
        {
            if (value is null || !EntityDetection.GetFuncIsPresent(value)(value.Data))
            {
                Instance._currentPokemon = null;
            }
            else
            {
                Instance._currentPokemon = value;
            }

            Instance.EmitSignalCurrentPokemonChanged();
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
        Instance = this;
        LocalizeUtil.InitializeStrings(GameInfo.CurrentLanguage, FakeSaveFile.Default);
    }

    public void EmitEventCurrentPokemonChanged() => EmitSignalCurrentPokemonChanged();

    public void GoToPreviousBox()
    {
        if (_saveFile is null)
            return;

        var totalBoxes = _saveFile.BoxesUnlocked == -1 ? _saveFile.BoxCount : _saveFile.BoxesUnlocked;
        var previousBoxIndex = (CurrentBoxIndex - 1 + totalBoxes) % totalBoxes;
        CurrentBoxIndex = previousBoxIndex;
    }

    public void GoToNextBox()
    {
        if (_saveFile is null)
            return;

        var totalBoxes = _saveFile.BoxesUnlocked == -1 ? _saveFile.BoxCount : _saveFile.BoxesUnlocked;
        var nextBoxIndex = (CurrentBoxIndex + 1) % totalBoxes;
        CurrentBoxIndex = nextBoxIndex;
    }

    public void LoadSave(string path)
    {
        _saveFile = SaveUtil.GetSaveFile(path) ?? throw new FileNotFoundException(path);
        _saveFile.Metadata.SetExtraInfo(path);

        // if (!SanityCheckSAV(ref sav))
        //     return true;

        if (_saveFile is SAV3 sav3)
            EReaderBerrySettings.LoadFrom(sav3);

        LocalizeUtil.InitializeStrings(GameInfo.CurrentLanguage, _saveFile);
        ParseSettings.InitFromSaveFileData(_saveFile); // physical GB, no longer used in logic
        RecentTrainerCache.SetRecentTrainer(_saveFile);

        GameInfo.FilteredSources = new FilteredGameDataSource(_saveFile, GameInfo.Sources);
        _saveFile.State.Edited = false;

        EmitSignalFileLoaded();
        EmitSignalPartyChanged();
        CurrentBoxIndex = 0;
        CurrentPokemon = null;
    }

    public void LoadPCData(string path)
    {
        if (_saveFile is null)
            return;

        var bytes = File.ReadAllBytes(path);
        var didSet = _saveFile.SetPCBinary(bytes);

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
