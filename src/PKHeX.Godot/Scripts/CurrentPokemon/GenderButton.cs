using Godot;
using PKHeX.Core;

namespace PKHeX.Godot.Scripts.CurrentPokemon;

public partial class GenderButton : TextureButton
{
    private Application _application = null!;

    public override void _Ready()
    {
        _application = GetNode<Application>(Application.NodePath);
        _application.CurrentPokemonChanged += CurrentPokemonChanged;
        _application.FileLoaded += OnFileLoaded;
    }

    private void CurrentPokemonChanged()
    {
        var genderIndex = _application.CurrentPokemon?.Pkm.Gender ?? (byte)Gender.Male;
        TextureNormal = GD.Load<Texture2D>(Assets.Sprites.Gender(genderIndex));
    }

    private void OnFileLoaded()
    {
        TextureNormal = GD.Load<Texture2D>(Assets.Sprites.Gender((byte)Gender.Genderless));
    }
}
