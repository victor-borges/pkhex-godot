using Godot;
using PKHeX.Core;

namespace PKHeX.Godot.Scripts.CurrentPokemon;

public partial class GenderButton : TextureButton
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
        var genderIndex = _gameData.CurrentPokemon?.Gender.Id ?? (int)Gender.Male;
        TextureNormal = GD.Load<Texture2D>($"res://Assets/Sprites/Gender/gender_{genderIndex}.webp");
    }

    private void OnFileLoaded(string _)
    {
        TextureNormal = GD.Load<Texture2D>($"res://Assets/Sprites/Gender/gender_2.webp");
    }
}
