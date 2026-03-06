using Godot;

namespace PKHeX.Godot.Scripts.CurrentPokemon;

public partial class RerollButton : Button
{
    private Application _application = null!;

    public override void _Ready()
    {
        _application = GetNode<Application>(Application.NodePath);

        Pressed += OnButtonPressed;
    }

    private void OnButtonPressed()
    {
        _application.CurrentPokemon?.RerollPID();
        _application.TriggerCurrentPokemonChanged();
    }
}
