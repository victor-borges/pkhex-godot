using Godot;
using PKHeX.Core;

namespace PKHeX.Godot.Scripts.CurrentPokemon;

public partial class FriendshipSpinBox : SpinBox
{
    private Application _application = null!;

    public override void _Ready()
    {
        _application = GetNode<Application>(Application.NodePath);
        _application.CurrentPokemonChanged += CurrentPokemonChanged;
        _application.FileLoaded += OnFileLoaded;

        ValueChanged += OnValueChanged;
    }

    private void OnValueChanged(double value)
    {
        _application.CurrentPokemon?.Friendship = (int)value;
        _application.TriggerCurrentPokemonChanged();
    }

    private void CurrentPokemonChanged()
    {
        if (_application.Game is null || _application.CurrentPokemon is null)
        {
            SetValueNoSignal(0);
        }
        else
        {
            var pkm = _application.CurrentPokemon.Pkm;

            SetMax(Experience.GetEXP(pkm.CurrentLevel, pkm.PersonalInfo.EXPGrowth));
            SetValueNoSignal(_application.CurrentPokemon.Friendship);
        }
    }

    private void OnFileLoaded()
    {
        SetValueNoSignal(0);
    }
}
