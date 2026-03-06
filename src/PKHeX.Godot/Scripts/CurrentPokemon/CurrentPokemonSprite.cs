using Godot;
using PKHeX.Godot.Scripts.Extensions;

namespace PKHeX.Godot.Scripts.CurrentPokemon;

public partial class CurrentPokemonSprite : TextureRect
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
        Texture = _application.CurrentPokemon != null && _application.CurrentPokemon.Species.Id != 0
            ? GD.Load<Texture2D>(_application.CurrentPokemon.GetSpritePath())
            : null;
    }

    private void OnFileLoaded()
    {
        Texture = null;
    }
}
