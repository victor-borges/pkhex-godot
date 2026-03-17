namespace PKHeX.Godot.Scripts.CurrentPokemon.Controls;

public partial class CurrentPokemonSprite : TextureRect
{
    public override void _Ready()
    {
        Application.Instance.CurrentPokemonChanged += CurrentPokemonChanged;
    }

    private void CurrentPokemonChanged()
    {
        Texture = Application.CurrentPokemon != null && Application.CurrentPokemon.Species != 0
            ? GD.Load<Texture2D>(Assets.PokemonSprite(Application.CurrentPokemon))
            : null;
    }
}
