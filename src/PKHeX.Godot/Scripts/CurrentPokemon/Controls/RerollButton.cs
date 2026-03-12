using PKHeX.Godot.Extensions;

namespace PKHeX.Godot.Scripts.CurrentPokemon.Controls;

public partial class RerollButton : Button
{
    private Application _application = null!;

    public override void _Ready()
    {
        _application = GetNode<Application>(Application.NodePath);
        _application.CurrentPokemonChanged += OnCurrentPokemonChanged;
        Pressed += OnButtonPressed;
    }

    private void OnButtonPressed()
    {
        if (_application.CurrentPokemon is null)
            return;

        _application.CurrentPokemon.RerollPID();
        _application.EmitEventCurrentPokemonChanged();
    }

    private void OnCurrentPokemonChanged()
    {
        Disabled = _application.CurrentPokemon is null;
    }
}
