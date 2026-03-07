namespace PKHeX.Godot.Scripts.CurrentPokemon;

public partial class LevelSpinBox : SpinBox
{
    private Application _application = null!;

    public override void _Ready()
    {
        _application = GetNode<Application>(Application.NodePath);
        _application.CurrentPokemonChanged += CurrentPokemonChanged;
        ValueChanged += OnValueChanged;
    }

    private void OnValueChanged(double value)
    {
        if (_application.CurrentPokemon is null)
            return;

        _application.CurrentPokemon.ChangeLevel((int)value);
        _application.TriggerCurrentPokemonChanged();
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
