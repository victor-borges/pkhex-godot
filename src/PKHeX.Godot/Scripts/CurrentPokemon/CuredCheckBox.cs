using Godot;

namespace PKHeX.Godot.Scripts.CurrentPokemon;

public partial class CuredCheckBox : CheckBox
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
            ButtonPressed = false;
        }
        else
        {
            ButtonPressed = _application.CurrentPokemon.Pkm.IsPokerusCured;
        }
    }

    private void OnFileLoaded()
    {
        ButtonPressed = false;
    }
}
