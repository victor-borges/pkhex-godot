namespace PKHeX.Godot.Scripts.CurrentPokemon.Controls;

public partial class LanguageOptionButton : OptionButton
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

        if (_application.Game is null)
            return;

        var languages = _application.Game.SaveFile.Generation >= 8
            ? GameInfo.FilteredSources.Languages.Append(GameInfo.Sources.Empty)
            : GameInfo.FilteredSources.Languages;

        foreach (var language in languages)
        {
            AddItem(language.Text, language.Value);
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
        }
        else
        {
            Selected = GetItemIndex(_application.CurrentPokemon.Pkm.Language);
            Disabled = false;
        }
    }
}
