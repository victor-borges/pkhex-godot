using Godot;
using PKHeX.Core;
using PKHeX.Facade.Pokemons;
using PKHeX.Godot.Scripts.Extensions;

namespace PKHeX.Godot.Scripts;

public partial class Slot : Button
{
    [Export] public int SlotIndex { get; set; }

    private SignalBus _signalBus = null!;
    private GameData _gameData = null!;

    private TextureRect _pokemonSprite = null!;
    private TextureRect _shinySprite = null!;
    private TextureRect _markerSprite = null!;
    private TextureRect _heldItemSprite = null!;

    private Panel _shinyPanel = null!;
    private Panel _markerPanel = null!;
    private Panel _heldItemPanel = null!;

    public override void _Ready()
    {
        _signalBus = GetNode<SignalBus>("/root/SignalBus");
        _gameData = GetNode<GameData>("/root/GameData");

        _pokemonSprite = GetNode<TextureRect>("%PokemonSprite");
        _shinySprite = GetNode<TextureRect>("%ShinySprite");
        _markerSprite = GetNode<TextureRect>("%MarkerSprite");
        _heldItemSprite = GetNode<TextureRect>("%HeldItemSprite");

        _shinyPanel = GetNode<Panel>("%ShinyPanel");
        _markerPanel = GetNode<Panel>("%MarkerPanel");
        _heldItemPanel = GetNode<Panel>("%HeldItemPanel");
    }

    private void OnButtonPressed()
    {
        _signalBus.EmitSignal(SignalBus.SignalName.BoxPokemonSelected, SlotIndex);
    }

    public void LoadSprites(Pokemon? pokemon)
    {
        _pokemonSprite.Visible = false;
        _shinyPanel.Visible = false;
        _markerPanel.Visible = false;
        _heldItemPanel.Visible = false;

        if (pokemon is null || pokemon.Species.Id is 0)
            return;

        if (pokemon.IsShiny)
        {
            _shinySprite.Texture = GD.Load<Texture2D>(pokemon.ShinyType switch
            {
                Shiny.AlwaysSquare => "res://Assets/Sprites/Overlays/rare_icon_2.webp",
                Shiny.AlwaysStar or _ => "res://Assets/Sprites/Overlays/rare_icon.webp"
            });

            _shinyPanel.Visible = true;
        }
        else
        {
            _shinyPanel.Visible = false;
        }

        if (pokemon.Pkm is IAlpha { IsAlpha: true })
        {
            _markerSprite.Texture = GD.Load<Texture2D>("res://Assets/Sprites/Overlays/alpha.webp");
            _markerPanel.Visible = true;
        }

        if (!pokemon.HeldItem.IsNone)
        {
            _heldItemSprite.Texture = GD.Load<Texture2D>($"res://Assets/Sprites/Items/item_{pokemon.HeldItem.Id:D4}.png");
            _heldItemPanel.Visible = true;
        }

        _pokemonSprite.Texture = GD.Load<Texture2D>(pokemon.GetSpritePath());
        _pokemonSprite.Visible = true;
    }
}
