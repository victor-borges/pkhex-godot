namespace PKHeX.Godot.Scripts.CurrentPokemon.Controls;

public partial class HeldItemOptionButton : OptionButton
{
    private Application _application = null!;

    public override void _Ready()
    {
        _application = GetNode<Application>(Application.NodePath);
        _application.CurrentPokemonChanged += CurrentPokemonChanged;
        _application.FileLoaded += OnFileLoaded;

        ItemSelected += OnItemSelected;

        if (_application.Game is not null)
            OnFileLoaded();
    }

    private void OnFileLoaded()
    {
        Clear();

        if (_application.Game is null)
            return;

        foreach (var gameItem in GameInfo.FilteredSources.Items)
        {
            AddItem(gameItem.Text, gameItem.Value);
        }
    }

    private void OnItemSelected(long index)
    {
        if (_application.CurrentPokemon is null)
            return;

        var id = GetItemId((int)index);
        _application.CurrentPokemon.HeldItem = id;
        _application.EmitEventCurrentPokemonChanged();
    }

    private void CurrentPokemonChanged()
    {
        if (_application.CurrentPokemon is null)
        {
            Disabled = true;
            Selected = -1;
        }
        else
        {
            Disabled = false;
            Selected = _application.CurrentPokemon.HeldItem is 0 ? -1 : GetItemIndex(_application.CurrentPokemon.HeldItem);
        }
    }
}
