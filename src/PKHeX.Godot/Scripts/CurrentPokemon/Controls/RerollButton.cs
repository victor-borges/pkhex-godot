using PKHeX.Godot.Extensions;

namespace PKHeX.Godot.Scripts.CurrentPokemon.Controls;

public partial class RerollButton : Button
{
    public override void _Ready()
    {
        Application.Instance.CurrentPokemonChanged += OnCurrentPokemonChanged;
        Pressed += OnButtonPressed;
    }

    private void OnButtonPressed()
    {
        if (Application.CurrentPokemon is null)
            return;

        Application.CurrentPokemon.RerollPID();
        Application.Instance.EmitEventCurrentPokemonChanged();
    }

    private void OnCurrentPokemonChanged()
    {
        Disabled = Application.CurrentPokemon is null;
    }
}
