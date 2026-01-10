using Godot;
using PKHeX.Core;
using PKHeX.Facade.Extensions;
using PKHeX.Facade.Pokemons;
using PKHeX.Godot.Scripts.Contants;
using PKHeX.Godot.Scripts.Extensions;

namespace PKHeX.Godot.Scripts;

public partial class Slot : Button
{
    [Export] public int SlotIndex { get; set; }
    [Export] public bool IsPartySlot { get; set; }

    private SignalBus _signalBus = null!;
    private GameData _gameData = null!;

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
        _signalBus = GetNode<SignalBus>("/root/SignalBus");
        _gameData = GetNode<GameData>("/root/GameData");

        _pokemonSprite = GetNode<TextureRect>("%PokemonSprite");
        _shinySprite = GetNode<TextureRect>("%ShinySprite");
        _markerSprite = GetNode<TextureRect>("%MarkerSprite");
        _heldItemSprite = GetNode<TextureRect>("%HeldItemSprite");
        _legalitySprite = GetNode<TextureRect>("%LegalitySprite");

        _shinyPanel = GetNode<Panel>("%ShinyPanel");
        _markerPanel = GetNode<Panel>("%MarkerPanel");
        _heldItemPanel = GetNode<Panel>("%HeldItemPanel");

        if (IsPartySlot) _signalBus.PartyChanged += OnPartyChanged;
        else _signalBus.BoxChanged += OnBoxChanged;
    }

    private void OnButtonPressed()
    {
        _signalBus.EmitSignal(IsPartySlot
            ? SignalBus.SignalName.PartyPokemonSelected
            : SignalBus.SignalName.BoxPokemonSelected,
            SlotIndex);
    }

    private void OnPartyChanged()
    {
        if (SlotIndex >= _gameData.Game?.Trainer.Party.Pokemons.Count)
            return;

        var pokemon = _gameData.Game?.Trainer.Party.Pokemons[SlotIndex];
        LoadSprites(pokemon);
    }

    private void OnBoxChanged(int boxIndex)
    {
        var index = (boxIndex * 30) + SlotIndex;
        var pokemon = _gameData.Game?.Trainer.PokemonBox.All[index];
        LoadSprites(pokemon);
    }

    private void LoadSprites(Pokemon? pokemon)
    {
        _pokemonSprite.Visible = false;
        _shinyPanel.Visible = false;
        _markerPanel.Visible = false;
        _heldItemPanel.Visible = false;
        _legalitySprite.Visible = false;

        if (pokemon is null || pokemon.Species.Id is 0)
            return;

        if (pokemon.IsShiny)
        {
            _shinySprite.Texture = GD.Load<Texture2D>(pokemon.ShinyType is Shiny.AlwaysSquare
                ? Resources.Sprites.Overlays.ShinySquare
                : Resources.Sprites.Overlays.Shiny);

            _shinyPanel.Visible = true;
        }
        else
        {
            _shinyPanel.Visible = false;
        }

        if (pokemon.Pkm is IAlpha { IsAlpha: true })
        {
            _markerSprite.Texture = GD.Load<Texture2D>(Resources.Sprites.Overlays.Alpha);
            _markerPanel.Visible = true;
        }

        if (!pokemon.HeldItem.IsNone)
        {
            _heldItemSprite.Texture = GD.Load<Texture2D>(Resources.Sprites.HeldItem(pokemon.HeldItem.Id));
            _heldItemPanel.Visible = true;
        }

        _legalitySprite.Visible = pokemon.Species.Id != 0 && !pokemon.Legality().Valid;

        _pokemonSprite.Texture = GD.Load<Texture2D>(pokemon.GetSpritePath());
        _pokemonSprite.Visible = true;
    }
}
