using Godot;
using PKHeX.Core;

namespace PKHeX.Godot.Scripts;

public partial class Slot : Button
{
    private TextureRect _pokemonSprite;
    private TextureRect _shinySprite;
    private TextureRect _markerSprite;
    private TextureRect _heldItemSprite;

    private Panel _shinyPanel;
    private Panel _markerPanel;
    private Panel _heldItemPanel;

    public override void _Ready()
    {
        _pokemonSprite = GetNode<TextureRect>("%PokemonSprite");
        _shinySprite = GetNode<TextureRect>("%ShinySprite");
        _markerSprite = GetNode<TextureRect>("%MarkerSprite");
        _heldItemSprite = GetNode<TextureRect>("%HeldItemSprite");

        _shinyPanel = GetNode<Panel>("%ShinyPanel");
        _markerPanel = GetNode<Panel>("%MarkerPanel");
        _heldItemPanel = GetNode<Panel>("%HeldItemPanel");
    }

    public void LoadSprites(PKM pkm)
    {
        var species = pkm?.Species ?? 0;

        if (species == 0)
        {
            _pokemonSprite.Visible = false;
            _shinyPanel.Visible = false;
            _markerPanel.Visible = false;
            _heldItemPanel.Visible = false;
            return;
        }

        var isShiny = pkm?.IsShiny ?? false;
        _shinyPanel.Visible = isShiny;

        var pokemonTexture = GD.Load<Texture2D>(isShiny
            ? $"res://Assets/Sprites/Pokemon/Shiny/{species}.png"
            : $"res://Assets/Sprites/Pokemon/{species}.png");

        _pokemonSprite.Texture = pokemonTexture;
        _pokemonSprite.Visible = true;
    }
}
