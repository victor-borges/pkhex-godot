using Godot;
using PKHeX.Core;

namespace PKHeX.Godot.Scripts.CurrentPokemon;

public partial class StatNatureMenuButton : MenuButton
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
            var statNature = _application.CurrentPokemon.Natures.StatNature;
            Text = GameInfo.Strings.Natures[(int)statNature];
        }
    }
}
