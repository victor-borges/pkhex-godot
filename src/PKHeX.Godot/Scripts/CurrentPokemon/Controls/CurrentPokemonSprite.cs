namespace PKHeX.Godot.Scripts.CurrentPokemon.Controls;

public partial class CurrentPokemonSprite : TextureRect
{
    private Application _application = null!;

    public override void _Ready()
    {
        _application = GetNode<Application>(Application.NodePath);
        _application.CurrentPokemonChanged += CurrentPokemonChanged;
    }

    private void CurrentPokemonChanged()
    {
        Texture = _application.CurrentPokemon != null && _application.CurrentPokemon.Species != 0
            ? GD.Load<Texture2D>(Assets.PokemonSprite(_application.CurrentPokemon))
            : null;
    }
}
