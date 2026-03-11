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

        foreach (var species in GameInfo.FilteredSources.Species)
        {
            AddItem(species.Text, species.Value);
        }
    }

    private void OnSpeciesSelected(long index)
    {
        if (_application.CurrentPokemon is null)
            return;

        var id = GetItemId((int)index);
        _application.CurrentPokemon.Pkm.Species = (ushort)id;
        _application.EmitEventCurrentPokemonChanged();
    }

    private void CurrentPokemonChanged()
    {
        if (_application.Game is null || _application.CurrentPokemon is null)
        {
            Disabled = true;
            Text = " ";
        }
        else
        {
            Disabled = false;
            var species = _application.CurrentPokemon.Species;
            Text = $"{species.Name} [{species.Id}]";
        }
    }
}
