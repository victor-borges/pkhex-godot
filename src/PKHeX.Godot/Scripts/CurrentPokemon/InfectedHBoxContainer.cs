using Godot;

namespace PKHeX.Godot.Scripts.CurrentPokemon;

public partial class InfectedHBoxContainer : HBoxContainer
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
            Visible = false;
        }
        else
        {
            Visible = _application.CurrentPokemon.Pkm.IsPokerusInfected;
        }
    }
}
