using Godot;

namespace PKHeX.Godot.Scripts.CurrentPokemon;

public partial class LevelSpinBox : SpinBox
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
            Value = 0;
        }
        else
        {
            Value = _application.CurrentPokemon.Level;
        }
    }

    private void OnFileLoaded()
    {
        Value = 0;
    }
}
