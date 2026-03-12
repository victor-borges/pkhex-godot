namespace PKHeX.Godot.Scripts.CurrentPokemon.Controls;

public partial class LanguageGBPKMOptionButton : OptionButton
{
    private Application _application = null!;

    public override void _Ready()
    {
        _application = GetNode<Application>(Application.NodePath);
        _application.CurrentPokemonChanged += CurrentPokemonChanged;

        ItemSelected += OnLanguageSelected;
    }

    private void OnLanguageSelected(long index)
    {
        if (_application.CurrentPokemon is not GBPKML pk1)
            return;

        var id = GetItemId((int)index);
        pk1.SetNotNicknamed(id);

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

        Clear();

        Dictionary<string, List<ComboItem>> nameLangMap = [];
        foreach (var language in GameInfo.FilteredSources.Languages)
        {
            if (language.Value == (int)LanguageID.Korean && _application.Game.Generation == 1)
                continue;

            var speciesName = SpeciesName.GetSpeciesNameGeneration(
                _application.CurrentPokemon.Species, language.Value, _application.Game.Generation);

            if (nameLangMap.TryGetValue(speciesName, out var ids))
                ids.Add(language);
            else
                nameLangMap[speciesName] = [language];
        }

        foreach (var (_, languages) in nameLangMap)
        {
            var text = languages.Select(lang => lang.Text).Aggregate((a, b) => $"{a} or {b}");
            var value = languages[0].Value;
            AddItem(text, value);

            if (_application.Game.Language == (int)LanguageID.Japanese)
            {
                SetItemDisabled(GetItemIndex(value), value != (int)LanguageID.Japanese);
            }
            else
            {
                SetItemDisabled(GetItemIndex(value), value == (int)LanguageID.Japanese);
            }

            if (languages.Select(l => l.Value).Contains(_application.CurrentPokemon.Language))
            {
                Selected = GetItemIndex(languages[0].Value);
            }
        }

        Disabled = false;
    }
}
