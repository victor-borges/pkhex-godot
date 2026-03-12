namespace PKHeX.Godot.Scripts.CurrentPokemon.Controls;

public partial class HeldItemMenuButton : MenuButton
{
    private Application _application = null!;

    public override void _Ready()
    {
        _application = GetNode<Application>(Application.NodePath);
        _application.CurrentPokemonChanged += CurrentPokemonChanged;
        _application.FileLoaded += OnFileLoaded;

        if (_application.Game is not null)
            OnFileLoaded();
    }

    private void CurrentPokemonChanged()
    {
        if (_application.CurrentPokemon is null || _application.CurrentPokemon.HeldItem is 0)
        {
            Disabled = true;
            Text = " ";
        }
        else
        {
            Disabled = false;
            Text = GameInfo.FilteredSources.Items[_application.CurrentPokemon.HeldItem].Text;
        }
    }

    private void OnFileLoaded()
    {
        var popup = GetPopup();
        popup.Clear(freeSubmenus: true);

        if (_application.Game is null || !GameInfo.FilteredSources.Items.Any())
            return;

        foreach (var gameItem in GameInfo.FilteredSources.Items)
        {
            popup.AddItem(gameItem.Text, gameItem.Value);
        }
    }
}
