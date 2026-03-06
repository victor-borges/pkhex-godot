using Godot;
using PKHeX.Core;
using PKHeX.Godot.Scripts.Contants;

namespace PKHeX.Godot.Scripts.CurrentPokemon;

public partial class ShinyButton : TextureButton
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
        TexturePressed = GD.Load<Texture2D>(_application.CurrentPokemon?.ShinyType is Shiny.AlwaysSquare
            ? Resources.Sprites.Overlays.ShinySquare
            : Resources.Sprites.Overlays.Shiny);

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
