using Godot;
using PKHeX.Core;

namespace PKHeX.Godot.Scripts.CurrentPokemon;

public partial class ShinyButton : TextureButton
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
        TexturePressed = GD.Load<Texture2D>(_application.CurrentPokemon?.ShinyType is Shiny.AlwaysSquare
            ? Assets.Sprites.Overlays.ShinySquare
            : Assets.Sprites.Overlays.Shiny);

        SetPressedNoSignal(_application.CurrentPokemon?.IsShiny ?? false);
    }

    private void OnFileLoaded()
    {
        ButtonPressed = false;
    }

    private void OnToggled(bool toggled)
    {
        _application.CurrentPokemon?.SetShiny(toggled);
        _application.TriggerCurrentPokemonChanged();
    }
}
