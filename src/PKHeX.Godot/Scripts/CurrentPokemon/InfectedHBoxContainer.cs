using Godot;

namespace PKHeX.Godot.Scripts.CurrentPokemon;

public partial class InfectedHBoxContainer : HBoxContainer
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
            Visible = false;
        }
        else
        {
            Visible = _application.CurrentPokemon.Pkm.IsPokerusInfected;
        }
    }

    private void OnFileLoaded()
    {
        Visible = false;
    }
}
