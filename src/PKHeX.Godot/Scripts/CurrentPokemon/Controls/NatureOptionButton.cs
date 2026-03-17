namespace PKHeX.Godot.Scripts.CurrentPokemon.Controls;

public partial class NatureOptionButton : OptionButton
{
    public override void _Ready()
    {
        Application.Instance.CurrentPokemonChanged += CurrentPokemonChanged;
        Application.Instance.FileLoaded += OnFileLoaded;

        ItemSelected += OnNatureSelected;

        if (Application.SaveFile is not null)
            OnFileLoaded();
    }

    private void OnFileLoaded()
    {
        Clear();

        foreach (var nature in GameInfo.FilteredSources.Natures)
        {
            AddItem(nature.Text, nature.Value);
        }
    }

    private void OnNatureSelected(long index)
    {
        var id = GetItemId((int)index);

        if (id >= (long)Nature.Random || Application.CurrentPokemon is null)
            return;

        Application.CurrentPokemon.Nature = (Nature)id;
        Application.Instance.EmitEventCurrentPokemonChanged();
    }

    private void CurrentPokemonChanged()
    {
        if (Application.SaveFile is null || Application.CurrentPokemon is null)
        {
            Disabled = true;
        }
        else
        {
            Disabled = false;
            Selected = GetItemIndex((int)Application.CurrentPokemon.Nature);
        }
    }
}
