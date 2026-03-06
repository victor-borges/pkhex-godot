using Godot;

namespace PKHeX.Godot.Scripts.CurrentPokemon;

public partial class LevelSpinBox : SpinBox
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
            Editable = false;
            SetValueNoSignal(0);
        }
        else
        {
            Editable = true;
            SetValueNoSignal(_application.CurrentPokemon.Level);
        }
    }
}
