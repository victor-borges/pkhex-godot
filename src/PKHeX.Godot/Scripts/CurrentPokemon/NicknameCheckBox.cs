using Godot;

namespace PKHeX.Godot.Scripts.CurrentPokemon;

public partial class NicknameCheckBox : CheckBox
{
    private GameData _gameData = null!;
    private LineEdit _nicknameLineEdit = null!;

    public override void _Ready()
    {
        _gameData = GetNode<GameData>("/root/GameData");
        _nicknameLineEdit = GetNode<LineEdit>("%NicknameLineEdit");

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
            var isNicknamed = _gameData.CurrentPokemon.Pkm.IsNicknamed;

            ButtonPressed = isNicknamed;
        }
    }

    private void OnFileLoaded(string _)
    {
        ButtonPressed = false;
    }
}
