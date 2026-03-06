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
    }

    private void CurrentPokemonChanged()
    {
        if (_application.CurrentPokemon is null)
        {
            Disabled = true;
            TextureNormal = GD.Load<Texture2D>(Assets.Sprites.Gender((byte)Gender.Genderless));
        }
        else
        {
            Disabled = false;
            var genderIndex = _application.CurrentPokemon.Pkm.Gender;
            TextureNormal = GD.Load<Texture2D>(Assets.Sprites.Gender(genderIndex));
        }
    }
}
