using PKHeX.Facade.Extensions;
using PKHeX.Facade.Pokemons;

namespace PKHeX.Godot.Scripts;

public partial class Slot : Button
{
    protected Pokemon? Pokemon;

    protected Application Application = null!;

    private TextureRect _pokemonSprite = null!;
    private TextureRect _shinySprite = null!;
    private TextureRect _markerSprite = null!;
    private TextureRect _heldItemSprite = null!;
    private TextureRect _legalitySprite = null!;

    private Panel _shinyPanel = null!;
    private Panel _markerPanel = null!;
    private Panel _heldItemPanel = null!;

    public override void _Ready()
    {
        Application = GetNode<Application>(Application.NodePath);

        _pokemonSprite = GetNode<TextureRect>("%PokemonSprite");
        _shinySprite = GetNode<TextureRect>("%ShinySprite");
        _markerSprite = GetNode<TextureRect>("%MarkerSprite");
        _heldItemSprite = GetNode<TextureRect>("%HeldItemSprite");
        _legalitySprite = GetNode<TextureRect>("%LegalitySprite");

        _shinyPanel = GetNode<Panel>("%ShinyPanel");
        _markerPanel = GetNode<Panel>("%MarkerPanel");
        _heldItemPanel = GetNode<Panel>("%HeldItemPanel");
    }

    protected void SetPokemon(Pokemon? pokemon)
    {
        Pokemon = pokemon;

        _pokemonSprite.Visible = false;
        _shinyPanel.Visible = false;
        _markerPanel.Visible = false;
        _heldItemPanel.Visible = false;
        _legalitySprite.Visible = false;

        if (pokemon is null || pokemon.Species.Id is 0)
            return;

        var pokemonSprite = Assets.PokemonSprite(pokemon);

        _legalitySprite.Visible = pokemon.Species.Id != 0 && !pokemon.Legality().Valid;

        if (pokemon.IsShiny)
        {
            _shinySprite.Texture = GD.Load<Texture2D>(pokemon.ShinyType is Shiny.AlwaysSquare
                ? Assets.Sprites.Overlays.ShinySquare
                : Assets.Sprites.Overlays.Shiny);

            _shinyPanel.Visible = true;
        }
        else
        {
            _shinyPanel.Visible = false;
        }

        if (pokemon.Pkm is IAlpha { IsAlpha: true })
        {
            _markerSprite.Texture = GD.Load<Texture2D>(Assets.Sprites.Overlays.Alpha);
            _markerPanel.Visible = true;
        }

        if (pokemon.Egg.IsEgg)
        {
            _heldItemSprite.Texture = GD.Load<Texture2D>(pokemonSprite);
            _heldItemPanel.Visible = true;

            _pokemonSprite.Texture = GD.Load<Texture2D>(Assets.Sprites.Egg);
            _pokemonSprite.Visible = true;
        }
        else if (!pokemon.HeldItem.IsNone)
        {
            _heldItemSprite.Texture = GD.Load<Texture2D>(Assets.Sprites.HeldItem(pokemon.HeldItem.Id));
            _heldItemPanel.Visible = true;
        }

        if (!pokemon.Egg.IsEgg)
        {
            _pokemonSprite.Texture = GD.Load<Texture2D>(pokemonSprite);
            _pokemonSprite.Visible = true;
        }
    }
}
