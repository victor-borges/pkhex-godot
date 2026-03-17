using PKHeX.Godot.Extensions;

namespace PKHeX.Godot.Scripts.CurrentPokemon.Controls;

public partial class LegalityButton : TextureButton
{
    public override void _Ready()
    {
        Application.Instance.CurrentPokemonChanged += CurrentPokemonChanged;
    }

    private void CurrentPokemonChanged()
    {
        if (Application.CurrentPokemon is null)
        {
            Visible = false;
            return;
        }

        // Visible = Application.CurrentPokemon.Species != 0;
        Visible = true;

        if (!Application.CurrentPokemon.Legality.Valid)
        {
            TextureNormal = GD.Load<Texture2D>(Assets.Sprites.Overlays.Illegal);
        }
        if (Application.CurrentPokemon.Format >= 8 && MoveInfo.IsDummiedMoveAny(Application.CurrentPokemon))
        {
            TextureNormal = GD.Load<Texture2D>(Assets.Sprites.Overlays.Illegal); // TODO: change to hint
        }
        else
        {
            TextureNormal = GD.Load<Texture2D>(Assets.Sprites.Overlays.Legal);
        }
    }
}
