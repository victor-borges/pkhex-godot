namespace PKHeX.Godot.Scripts.CurrentPokemon.Controls;

public partial class EXPSpinBox : SpinBox
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

        _application.CurrentPokemon.Experience = (uint)value;
        _application.EmitEventCurrentPokemonChanged();
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
            var pkm = _application.CurrentPokemon.Pkm;

            SetMax(Experience.GetEXP(Experience.MaxLevel, pkm.PersonalInfo.EXPGrowth));
            SetMin(Experience.GetEXP(Experience.MinLevel, pkm.PersonalInfo.EXPGrowth));
            SetValueNoSignal(_application.CurrentPokemon.Experience);
        }
    }
}
