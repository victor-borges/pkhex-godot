using PKHeX.Facade.Extensions;

namespace PKHeX.Godot.Scripts.CurrentPokemon.Controls;

public partial class LegalityButton : TextureButton
{
    private Application _application = null!;

    public override void _Ready()
    {
        _application = GetNode<Application>(Application.NodePath);
        _application.CurrentPokemonChanged += CurrentPokemonChanged;
    }

    private void CurrentPokemonChanged()
    {
        if (_application.CurrentPokemon is null)
        {
            Visible = false;
            return;
        }

        Visible = _application.CurrentPokemon.Species.Id != 0;
        var la = _application.CurrentPokemon.Legality();
        var valid = la.Valid;

        if (!valid)
        {
            TextureNormal = GD.Load<Texture2D>(Assets.Sprites.Overlays.Illegal);
        }
        if (_application.CurrentPokemon.Pkm.Format >= 8 && MoveInfo.IsDummiedMoveAny(_application.CurrentPokemon.Pkm))
        {
            TextureNormal = GD.Load<Texture2D>(Assets.Sprites.Overlays.Illegal); // TODO: change to hint
        }
        else
        {
            TextureNormal = GD.Load<Texture2D>(Assets.Sprites.Overlays.Legal);
        }
    }
}
