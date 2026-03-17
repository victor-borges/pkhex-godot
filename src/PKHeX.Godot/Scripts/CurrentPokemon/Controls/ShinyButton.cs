using PKHeX.Godot.Extensions;

namespace PKHeX.Godot.Scripts.CurrentPokemon.Controls;

public partial class ShinyButton : Button
{
    public override void _Ready()
    {
        Application.Instance.CurrentPokemonChanged += CurrentPokemonChanged;

        Toggled += OnToggled;
    }

    private void OnToggled(bool toggled)
    {
        if (Application.CurrentPokemon is null)
            return;

        Application.CurrentPokemon.SetIsShiny(toggled);
        Application.Instance.EmitEventCurrentPokemonChanged();
    }

    private void CurrentPokemonChanged()
    {
        if (Application.CurrentPokemon is null)
        {
            Disabled = true;
        }
        else
        {
            var isShiny = Application.CurrentPokemon.IsShiny;
            var shinyType = Application.CurrentPokemon.ShinyType;
            var squareShinyExists = ShinyExtensions.IsSquareShinyExist(Application.CurrentPokemon);

            Disabled = false;
            SetPressedNoSignal(isShiny);

            if (!isShiny)
            {
                Icon = GD.Load<Texture2D>(Assets.Sprites.Overlays.ShinyFrame);
            }
            else
            {
                Icon = GD.Load<Texture2D>(shinyType is Shiny.AlwaysSquare && squareShinyExists
                    ? Assets.Sprites.Overlays.ShinySquare
                    : Assets.Sprites.Overlays.Shiny);
            }
        }
    }
}
