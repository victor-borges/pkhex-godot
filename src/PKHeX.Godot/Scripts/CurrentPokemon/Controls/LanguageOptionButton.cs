namespace PKHeX.Godot.Scripts.CurrentPokemon.Controls;

public partial class LanguageOptionButton : OptionButton
{
    public override void _Ready()
    {
        Application.Instance.CurrentPokemonChanged += CurrentPokemonChanged;
        Application.Instance.FileLoaded += OnFileLoaded;

        ItemSelected += OnLanguageSelected;

        if (Application.SaveFile is not null)
            OnFileLoaded();
    }

    private void OnFileLoaded()
    {
        Clear();

        if (Application.SaveFile is null)
            return;

        foreach (var language in GameInfo.FilteredSources.Languages)
        {
            AddItem(language.Text, language.Value);
        }
    }

    private void OnLanguageSelected(long index)
    {
        if (Application.CurrentPokemon is null)
            return;

        var id = GetItemId((int)index);
        Application.CurrentPokemon.Language = id;

        if (!Application.CurrentPokemon.IsNicknamed)
            Application.CurrentPokemon.SetDefaultNickname();

        Application.Instance.EmitEventCurrentPokemonChanged();
    }

    private void CurrentPokemonChanged()
    {
        if (Application.SaveFile is null || Application.CurrentPokemon is null)
        {
            Selected = -1;
            Disabled = true;
        }
        else
        {
            Selected = GetItemIndex(Application.CurrentPokemon.Language);
            Disabled = false;
        }
    }
}
