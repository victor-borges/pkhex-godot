using Godot;

namespace PKHeX.Godot.Scripts.CurrentPokemon;

public partial class IsEggCheckBox : CheckBox
{
    private SignalBus _signalBus = null!;
    private GameData _gameData = null!;

    public override void _Ready()
    {
        _gameData = GetNode<GameData>("/root/GameData");
        _signalBus = GetNode<SignalBus>("/root/SignalBus");

        _signalBus.CurrentPokemonChanged += CurrentPokemonChanged;
        _signalBus.FileLoaded += OnFileLoaded;
        Toggled += OnToggled;
    }

    private void OnToggled(bool toggled)
    {
        _gameData.CurrentPokemon?.Egg.IsEgg = toggled;
        _signalBus.EmitSignal(SignalBus.SignalName.CurrentPokemonChanged);
    }

    private void CurrentPokemonChanged()
    {
        if (_gameData.Game is null || _gameData.CurrentPokemon is null)
        {
            ButtonPressed = false;
        }
        else
        {
            ButtonPressed = _gameData.CurrentPokemon.Pkm.IsEgg;
        }
    }

    private void OnFileLoaded(string _)
    {
        ButtonPressed = false;
    }
}
