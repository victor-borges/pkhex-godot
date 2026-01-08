using Godot;
using PKHeX.Facade.Extensions;

namespace PKHeX.Godot.Scripts.CurrentPokemon;

public partial class InvalidSprite : TextureButton
{
    private SignalBus _signalBus = null!;
    private GameData _gameData = null!;
    private AcceptDialog _legalityDialog = null;

    public override void _Ready()
    {
        _gameData = GetNode<GameData>("/root/GameData");
        _signalBus = GetNode<SignalBus>("/root/SignalBus");
        _legalityDialog = GetNode<AcceptDialog>("LegalityDialog");

        _signalBus.CurrentPokemonChanged += CurrentPokemonChanged;
        _legalityDialog.AddButton("Full Report", action: "FullReport");
    }

    private void CurrentPokemonChanged()
    {
        Visible =
            _gameData.CurrentPokemon is not null
            && _gameData.CurrentPokemon.Species.Id != 0
            && !_gameData.CurrentPokemon.Legality().Valid;
    }

    private void OnFileLoaded(string _)
    {
        Visible = false;
    }
}
