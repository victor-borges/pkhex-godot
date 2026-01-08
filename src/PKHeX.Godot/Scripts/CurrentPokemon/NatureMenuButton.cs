using Godot;
using PKHeX.Core;

namespace PKHeX.Godot.Scripts.CurrentPokemon;

public partial class NatureMenuButton : MenuButton
{
    private SignalBus _signalBus = null!;
    private GameData _gameData = null!;

    public override void _Ready()
    {
        _gameData = GetNode<GameData>("/root/GameData");
        _signalBus = GetNode<SignalBus>("/root/SignalBus");

        _signalBus.CurrentPokemonChanged += CurrentPokemonChanged;
        _signalBus.FileLoaded += OnFileLoaded;

        var popup = GetPopup();

        foreach (var nature in GameInfo.Strings.Natures)
            popup.AddItem(nature);
    }

    private void CurrentPokemonChanged()
    {
        if (_gameData.Game is null || _gameData.CurrentPokemon is null)
        {
            Text = " ";
        }
        else
        {
            var nature = _gameData.CurrentPokemon.Natures.Nature;
            Text = GameInfo.Strings.Natures[(int)nature];
        }
    }

    private void OnFileLoaded(string _)
    {
        Text = " ";
    }
}
