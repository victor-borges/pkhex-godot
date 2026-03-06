using Godot;
using PKHeX.Core;

namespace PKHeX.Godot.Scripts.CurrentPokemon;

public partial class GenderButton : TextureButton
{
    private Application _application = null!;

    public override void _Ready()
    {
        _application = GetNode<Application>("/root/Application");

        _application.CurrentPokemonChanged += CurrentPokemonChanged;
        _application.FileLoaded += OnFileLoaded;
    }

    private void CurrentPokemonChanged()
    {
        var genderIndex = _application.CurrentPokemon?.Gender.Id ?? (int)Gender.Male;
        TextureNormal = GD.Load<Texture2D>($"res://Assets/Sprites/Gender/gender_{genderIndex}.webp");
    }

    private void OnFileLoaded()
    {
        TextureNormal = GD.Load<Texture2D>($"res://Assets/Sprites/Gender/gender_2.webp");
    }
}
