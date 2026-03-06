using Godot;

namespace PKHeX.Godot.Scripts.CurrentPokemon;

public partial class CuredCheckBox : CheckBox
{
    private GameData _gameData = null!;

    public override void _Ready()
    {
        _gameData = GetNode<GameData>("/root/GameData");
        _gameData.CurrentPokemonChanged += CurrentPokemonChanged;
        _gameData.FileLoaded += OnFileLoaded;
    }

    private void CurrentPokemonChanged()
    {
        if (_gameData.Game is null || _gameData.CurrentPokemon is null)
        {
            ButtonPressed = false;
        }
        else
        {
            ButtonPressed = _gameData.CurrentPokemon.Pkm.IsPokerusCured;
        }
    }

    private void OnFileLoaded(string _)
    {
        ButtonPressed = false;
    }
}
