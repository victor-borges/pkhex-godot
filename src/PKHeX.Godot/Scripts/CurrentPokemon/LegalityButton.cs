using Godot;
using PKHeX.Facade.Extensions;

namespace PKHeX.Godot.Scripts.CurrentPokemon;

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
        TextureNormal = GD.Load<Texture2D>(_application.CurrentPokemon.Legality().Valid
            ? "res://Assets/Sprites/Overlays/legal.webp"
            : "res://Assets/Sprites/Overlays/illegal.webp");
    }

    private void OnFileLoaded()
    {
        Visible = false;
    }
}
