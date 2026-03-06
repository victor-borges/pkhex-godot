using Godot;

namespace PKHeX.Godot.Scripts.CurrentPokemon;

public partial class SpeciesMenuButton : MenuButton
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
            Text = " ";
        }
        else
        {
            var species = _gameData.CurrentPokemon.Species;
            Text = $"{species.Name} [{species.Id}]";
        }
    }

    private void OnFileLoaded(string _)
    {
        Text = " ";
    }
}
