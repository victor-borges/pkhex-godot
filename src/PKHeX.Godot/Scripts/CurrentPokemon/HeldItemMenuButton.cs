using System.Linq;
using Godot;
using PKHeX.Core;
using PKHeX.Facade.Repositories;

namespace PKHeX.Godot.Scripts.CurrentPokemon;

public partial class HeldItemMenuButton : MenuButton
{
    private Application _application = null!;
    private ItemRepository? _itemRepository;

    public override void _Ready()
    {
        _application = GetNode<Application>("/root/Application");

        _application.CurrentPokemonChanged += CurrentPokemonChanged;
        _application.FileLoaded += OnFileLoaded;
    }

    private void CurrentPokemonChanged()
    {
        Text = _application.CurrentPokemon?.HeldItem.IsNone is false
            ? _application.CurrentPokemon.HeldItem.Name
            : " ";
    }

    private void OnFileLoaded()
    {
        Text = " ";
        _itemRepository = new ItemRepository(_application.Game?.SaveFile ?? FakeSaveFile.Default);
        PopulateHeldItemMenu();
    }

    private void PopulateHeldItemMenu()
    {
        var popup = GetPopup();
        popup.Clear(freeSubmenus: true);
        Text = " ";

        if (_application.Game is null
            || _itemRepository?.GameItems is null
            || !_itemRepository.GameItems.Any())
            return;

        foreach (var gameItem in _itemRepository.GameItems)
        {
            popup.AddItem(gameItem.Name);
        }
    }
}
