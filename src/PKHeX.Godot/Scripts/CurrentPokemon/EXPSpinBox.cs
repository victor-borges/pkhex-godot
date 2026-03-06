using Godot;

namespace PKHeX.Godot.Scripts.CurrentPokemon;

public partial class EXPSpinBox : SpinBox
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
        if (_application.Game is null || _application.CurrentPokemon is null)
        {
            Value = 0;
        }
        else
        {
            Value = _application.CurrentPokemon.Experience;
        }
    }

    private void OnFileLoaded()
    {
        Value = 0;
    }
}
