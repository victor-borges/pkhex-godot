using Godot;
using PKHeX.Facade.Extensions;

namespace PKHeX.Godot.Scripts.CurrentPokemon;

public partial class LegalityButton : TextureButton
{
    private SignalBus _signalBus = null!;
    private GameData _gameData = null!;

    public override void _Ready()
    {
        _gameData = GetNode<GameData>("/root/GameData");
        _signalBus = GetNode<SignalBus>("/root/SignalBus");

        _signalBus.CurrentPokemonChanged += CurrentPokemonChanged;
    }

    private void CurrentPokemonChanged()
    {
        if (_gameData.CurrentPokemon is null)
        {
            Visible = false;
            return;
        }

        Visible = _gameData.CurrentPokemon.Species.Id != 0;
        TextureNormal = GD.Load<Texture2D>(_gameData.CurrentPokemon.Legality().Valid
            ? "res://Assets/Sprites/Overlays/legal.webp"
            : "res://Assets/Sprites/Overlays/illegal.webp");
    }

    private void OnFileLoaded(string _)
    {
        Visible = false;
    }
}
