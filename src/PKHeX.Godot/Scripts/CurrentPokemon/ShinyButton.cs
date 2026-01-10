using Godot;
using PKHeX.Core;
using PKHeX.Godot.Scripts.Contants;

namespace PKHeX.Godot.Scripts.CurrentPokemon;

public partial class ShinyButton : TextureButton
{
    private SignalBus _signalBus = null!;
    private GameData _gameData = null!;

    public override void _Ready()
    {
        _gameData = GetNode<GameData>("/root/GameData");
        _signalBus = GetNode<SignalBus>("/root/SignalBus");

        _signalBus.CurrentPokemonChanged += CurrentPokemonChanged;
        _signalBus.FileLoaded += OnFileLoaded;
    }

    private void CurrentPokemonChanged()
    {
        TexturePressed = GD.Load<Texture2D>(_gameData.CurrentPokemon?.ShinyType is Shiny.AlwaysSquare
            ? Resources.Sprites.Overlays.ShinySquare
            : Resources.Sprites.Overlays.Shiny);

        SetPressedNoSignal(_gameData.CurrentPokemon?.IsShiny ?? false);
    }

    private void OnFileLoaded(string _)
    {
        ButtonPressed = false;
    }

    private void OnToggled(bool toggled)
    {
        _gameData.CurrentPokemon?.SetShiny(toggled);
        _signalBus.EmitSignal(SignalBus.SignalName.CurrentPokemonChanged);
    }
}
