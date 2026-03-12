namespace PKHeX.Godot.Scripts.CurrentPokemon.Controls;

public partial class SpeciesOptionButton : OptionButton
{
    private Application _application = null!;

    public override void _Ready()
    {
        _application = GetNode<Application>(Application.NodePath);
        _application.CurrentPokemonChanged += CurrentPokemonChanged;
        _application.FileLoaded += OnFileLoaded;

        ItemSelected += OnSpeciesSelected;

        if (_application.Game is not null)
            OnFileLoaded();
    }

    private void OnFileLoaded()
    {
        Clear();

        if (_application.Game is null)
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
        if (_application.CurrentPokemon is null)
            return;

        var id = GetItemId((int)index);
        _application.CurrentPokemon.Species = (ushort)id;
        _application.EmitEventCurrentPokemonChanged();
    }

    private void CurrentPokemonChanged()
    {
        if (_application.Game is null || _application.CurrentPokemon is null)
        {
            Disabled = true;
            Selected = -1;
        }
        else
        {
            Disabled = false;
            Selected = GetItemIndex(_application.CurrentPokemon.Species);
        }
    }
}
