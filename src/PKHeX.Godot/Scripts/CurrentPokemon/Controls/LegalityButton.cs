using PKHeX.Godot.Extensions;

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

        Visible = _application.CurrentPokemon.Species != 0;

        if (!_application.CurrentPokemon.Legality.Valid)
        {
            TextureNormal = GD.Load<Texture2D>(Assets.Sprites.Overlays.Illegal);
        }
        if (_application.CurrentPokemon.Format >= 8 && MoveInfo.IsDummiedMoveAny(_application.CurrentPokemon))
        {
            TextureNormal = GD.Load<Texture2D>(Assets.Sprites.Overlays.Illegal); // TODO: change to hint
        }
        else
        {
            TextureNormal = GD.Load<Texture2D>(Assets.Sprites.Overlays.Legal);
        }
    }
}
