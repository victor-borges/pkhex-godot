using Godot;

namespace PKHeX.Godot.Scripts.CurrentPokemon;

public partial class SpeciesMenuButton : MenuButton
{
    private Application _application = null!;

    public override void _Ready()
    {
        _application = GetNode<Application>(Application.NodePath);
        _application.CurrentPokemonChanged += CurrentPokemonChanged;
    }

    private void CurrentPokemonChanged()
    {
        if (_application.Game is null || _application.CurrentPokemon is null)
        {
            Disabled = true;
            Text = " ";
        }
        else
        {
            Disabled = false;
            var species = _application.CurrentPokemon.Species;
            Text = $"{species.Name} [{species.Id}]";
        }
    }
}
