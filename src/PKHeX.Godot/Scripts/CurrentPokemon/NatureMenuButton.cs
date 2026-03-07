namespace PKHeX.Godot.Scripts.CurrentPokemon;

public partial class NatureMenuButton : MenuButton
{
    private Application _application = null!;

    public override void _Ready()
    {
        _application = GetNode<Application>(Application.NodePath);
        _application.CurrentPokemonChanged += CurrentPokemonChanged;

        var popup = GetPopup();

        foreach (var nature in GameInfo.Strings.Natures)
            popup.AddItem(nature);
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
            var nature = _application.CurrentPokemon.Natures.Nature;
            Text = GameInfo.Strings.Natures[(int)nature];
        }
    }
}
