using System.IO;
using System.Linq;
using System.Text;
using Godot;
using PKHeX.Core;
using PKHeX.Facade.Repositories;

namespace PKHeX.Godot.Scripts.CurrentPokemon;

public partial class HeldItemMenuButton : MenuButton
{
    private SignalBus _signalBus = null!;
    private GameData _gameData = null!;
    private ItemRepository? _itemRepository;

    public override void _Ready()
    {
        _gameData = GetNode<GameData>("/root/GameData");
        _signalBus = GetNode<SignalBus>("/root/SignalBus");

        _signalBus.CurrentPokemonChanged += CurrentPokemonChanged;
        _signalBus.FileLoaded += OnFileLoaded;
    }

    private void CurrentPokemonChanged()
    {
        Text = _gameData.CurrentPokemon?.HeldItem.IsNone is false
            ? _gameData.CurrentPokemon.HeldItem.Name
            : " ";
    }

    private void OnFileLoaded(string path)
    {
        Text = " ";
        _itemRepository = new ItemRepository(_gameData.Game?.SaveFile ?? FakeSaveFile.Default);
        PopulateHeldItemMenu();
    }

    private void PopulateHeldItemMenu()
    {
        var popup = GetPopup();
        popup.Clear(freeSubmenus: true);
        Text = " ";

        if (_gameData.Game is null
            || _itemRepository?.GameItems is null
            || !_itemRepository.GameItems.Any())
            return;

        foreach (var gameItem in _itemRepository.GameItems)
        {
            popup.AddItem(gameItem.Name);
        }
    }
}
