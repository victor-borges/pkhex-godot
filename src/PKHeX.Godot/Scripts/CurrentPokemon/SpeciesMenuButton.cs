using Godot;

namespace PKHeX.Godot.Scripts.CurrentPokemon;

public partial class SpeciesMenuButton : MenuButton
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
        if (_application.Game is null || _application.CurrentPokemon is null)
        {
            Text = " ";
        }
        else
        {
            var species = _application.CurrentPokemon.Species;
            Text = $"{species.Name} [{species.Id}]";
        }
    }

    private void OnFileLoaded()
    {
        Text = " ";
    }
}
