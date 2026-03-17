namespace PKHeX.Godot.Scripts.CurrentPokemon.Controls;

public partial class LanguageGBPKMOptionButton : OptionButton
{
    public override void _Ready()
    {
        Application.Instance.CurrentPokemonChanged += CurrentPokemonChanged;

        ItemSelected += OnLanguageSelected;
    }

    private void OnLanguageSelected(long index)
    {
        if (Application.CurrentPokemon is not GBPKML pk1)
            return;

        var id = GetItemId((int)index);
        pk1.SetNotNicknamed(id);

        Application.Instance.EmitEventCurrentPokemonChanged();
    }

    private void CurrentPokemonChanged()
    {
        if (Application.SaveFile is null || Application.CurrentPokemon is null)
        {
            Selected = -1;
            Disabled = true;
            return;
        }

        Clear();

        Dictionary<string, List<ComboItem>> nameLangMap = [];
        foreach (var language in GameInfo.FilteredSources.Languages)
        {
            if (language.Value == (int)LanguageID.Korean && Application.SaveFile.Generation == 1)
                continue;

            var speciesName = SpeciesName.GetSpeciesNameGeneration(
                Application.CurrentPokemon.Species, language.Value, Application.SaveFile.Generation);

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

            if (Application.SaveFile.Language == (int)LanguageID.Japanese)
            {
                SetItemDisabled(GetItemIndex(value), value != (int)LanguageID.Japanese);
            }
            else
            {
                SetItemDisabled(GetItemIndex(value), value == (int)LanguageID.Japanese);
            }

            if (languages.Select(l => l.Value).Contains(Application.CurrentPokemon.Language))
            {
                Selected = GetItemIndex(languages[0].Value);
            }
        }

        Disabled = false;
    }
}
