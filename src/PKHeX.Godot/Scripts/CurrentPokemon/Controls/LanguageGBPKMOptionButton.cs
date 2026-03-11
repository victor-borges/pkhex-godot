namespace PKHeX.Godot.Scripts.CurrentPokemon.Controls;

public partial class LanguageGBPKMOptionButton : OptionButton
{
    private Application _application = null!;

    public override void _Ready()
    {
        _application = GetNode<Application>(Application.NodePath);
        _application.CurrentPokemonChanged += CurrentPokemonChanged;
        _application.FileLoaded += OnFileLoaded;

        ItemSelected += OnLanguageSelected;

        if (_application.Game is not null)
            OnFileLoaded();
    }

    private void OnFileLoaded()
    {
        Clear();

        if (_application.Game is null || _application.Game.SaveFile.Generation > 2)
            return;

        var languages = GameInfo.FilteredSources.Languages;
        var gameLanguage = _application.Game.SaveFile.Language;

        foreach (var language in languages)
        {
            AddItem(language.Text, language.Value);

            if (gameLanguage == (int)LanguageID.Japanese)
            {
                SetItemDisabled(GetItemIndex(language.Value), language.Value != (int)LanguageID.Japanese);
            }
            else
            {
                SetItemDisabled(GetItemIndex(language.Value), language.Value == (int)LanguageID.Japanese);
            }
        }
    }

    private void OnLanguageSelected(long index)
    {
        if (_application.CurrentPokemon is null)
            return;

        var id = GetItemId((int)index);
        _application.CurrentPokemon.Pkm.Language = id;
        _application.EmitEventCurrentPokemonChanged();
    }

    private void CurrentPokemonChanged()
    {
        if (_application.Game is null || _application.CurrentPokemon is null)
        {
            Selected = -1;
            Disabled = true;
            return;
        }

        Disabled = false;

        var language = _application.CurrentPokemon.Pkm.Language;

        if (_application.CurrentPokemon.Pkm is PK1 pk1)
        {
            language = _application.Game.SaveFile.Language;
            language = pk1.IsSpeciesNameMatch(language) ? language : pk1.GuessedLanguage(language);
        }

        Selected = GetItemIndex(language);
    }
}
