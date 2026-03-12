namespace PKHeX.Godot.Scripts.CurrentPokemon.Controls;

public partial class StatNatureOptionButton : OptionButton
{
    private Application _application = null!;

    public override void _Ready()
    {
        _application = GetNode<Application>(Application.NodePath);
        _application.CurrentPokemonChanged += CurrentPokemonChanged;
        _application.FileLoaded += OnFileLoaded;

        ItemSelected += OnNatureSelected;

        if (_application.Game is not null)
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

        if (id >= (long)Nature.Random || _application.CurrentPokemon is null)
            return;

        _application.CurrentPokemon.StatNature = (Nature)id;
        _application.EmitEventCurrentPokemonChanged();
    }

    private void CurrentPokemonChanged()
    {
        if (_application.Game is null || _application.CurrentPokemon is null)
        {
            Disabled = true;
        }
        else
        {
            Disabled = false;
            Selected = GetItemIndex((int)_application.CurrentPokemon.StatNature);
        }
    }
}
