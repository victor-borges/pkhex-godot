namespace PKHeX.Godot.Scripts.CurrentPokemon.Controls;

public partial class HeldItemOptionButton : OptionButton
{
    public override void _Ready()
    {
        Application.Instance.CurrentPokemonChanged += CurrentPokemonChanged;
        Application.Instance.FileLoaded += OnFileLoaded;

        ItemSelected += OnItemSelected;

        if (Application.SaveFile is not null)
            OnFileLoaded();
    }

    private void OnFileLoaded()
    {
        Clear();

        if (Application.SaveFile is null)
            return;

        foreach (var gameItem in GameInfo.FilteredSources.Items)
        {
            AddItem(gameItem.Text, gameItem.Value);
        }
    }

    private void OnItemSelected(long index)
    {
        if (Application.CurrentPokemon is null)
            return;

        var id = GetItemId((int)index);
        Application.CurrentPokemon.HeldItem = id;
        Application.Instance.EmitEventCurrentPokemonChanged();
    }

    private void CurrentPokemonChanged()
    {
        if (Application.CurrentPokemon is null)
        {
            Disabled = true;
            Selected = -1;
        }
        else
        {
            Disabled = false;
            Selected = GetItemIndex(Application.CurrentPokemon.HeldItem);
        }
    }
}
