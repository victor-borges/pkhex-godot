namespace PKHeX.Godot.Scripts.CurrentPokemon.Controls;

public partial class SpeciesOptionButton : OptionButton
{
    public override void _Ready()
    {
        Application.Instance.CurrentPokemonChanged += CurrentPokemonChanged;
        Application.Instance.FileLoaded += OnFileLoaded;

        ItemSelected += OnSpeciesSelected;

        if (Application.SaveFile is not null)
            OnFileLoaded();
    }

    private void OnFileLoaded()
    {
        Clear();

        if (Application.SaveFile is null)
            return;

        var speciesSource = GameInfo.FilteredSources.Species
            .Where(s => s.Value is not 0)
            .OrderBy(s => s.Value);

        foreach (var species in speciesSource)
        {
            AddItem($"{species.Text} [{species.Value}]", species.Value);
        }
    }

    private void OnSpeciesSelected(long index)
    {
        if (Application.CurrentPokemon is null)
            return;

        var id = GetItemId((int)index);
        Application.CurrentPokemon.Species = (ushort)id;
        Application.Instance.EmitEventCurrentPokemonChanged();
    }

    private void CurrentPokemonChanged()
    {
        if (Application.SaveFile is null || Application.CurrentPokemon is null)
        {
            Disabled = true;
            Selected = -1;
        }
        else
        {
            Disabled = false;
            Selected = GetItemIndex(Application.CurrentPokemon.Species);
        }
    }
}
