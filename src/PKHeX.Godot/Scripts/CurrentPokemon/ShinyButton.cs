namespace PKHeX.Godot.Scripts.CurrentPokemon;

public partial class ShinyButton : Button
{
    private Application _application = null!;

    public override void _Ready()
    {
        _application = GetNode<Application>(Application.NodePath);
        _application.CurrentPokemonChanged += CurrentPokemonChanged;

        Toggled += OnToggled;
    }

    private void OnToggled(bool toggled)
    {
        if (_application.CurrentPokemon is null)
            return;

        _application.CurrentPokemon.SetShiny(toggled);
        _application.EmitEventCurrentPokemonChanged();
    }

    private void CurrentPokemonChanged()
    {
        if (_application.CurrentPokemon is null)
        {
            Disabled = true;
        }
        else
        {
            var isShiny = _application.CurrentPokemon.IsShiny;
            var shinyType = _application.CurrentPokemon.ShinyType;
            var squareShinyExist = ShinyExtensions.IsSquareShinyExist(_application.CurrentPokemon.Pkm);

            Disabled = false;
            SetPressedNoSignal(isShiny);

            if (!isShiny)
            {
                Icon = GD.Load<Texture2D>(Assets.Sprites.Overlays.ShinyFrame);
            }
            else
            {
                Icon = GD.Load<Texture2D>(shinyType is Shiny.AlwaysSquare && squareShinyExist
                    ? Assets.Sprites.Overlays.ShinySquare
                    : Assets.Sprites.Overlays.Shiny);
            }
        }
    }
}
