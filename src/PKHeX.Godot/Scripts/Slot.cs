using Godot;
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
        if (pokemon is null || pokemon.Species.Id is 0)
        {
            _pokemonSprite.Visible = false;
            _shinyPanel.Visible = false;
            _markerPanel.Visible = false;
            _heldItemPanel.Visible = false;
            return;
        }

        _shinyPanel.Visible = pokemon.IsShiny;

        if (!pokemon.HeldItem.IsNone)
        {
            _heldItemSprite.Texture = GD.Load<Texture2D>($"res://Assets/Sprites/Items/item_{pokemon.HeldItem.Id:D4}.png");
            _heldItemPanel.Visible = true;
        }

        _pokemonSprite.Texture = GD.Load<Texture2D>(pokemon.GetSpritePath());
        _pokemonSprite.Visible = true;
    }
}
