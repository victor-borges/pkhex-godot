using Godot;
using PKHeX.Godot.Scripts.Extensions;

namespace PKHeX.Godot.Scripts.CurrentPokemon;

public partial class CurrentPokemonSprite : TextureRect
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
        Texture = _gameData.CurrentPokemon != null && _gameData.CurrentPokemon.Species.Id != 0
            ? GD.Load<Texture2D>(_gameData.CurrentPokemon.GetSpritePath())
            : null;
    }

    private void OnFileLoaded(string _)
    {
        Texture = null;
    }
}
