using System.Linq;
using Godot;
using PKHeX.Core;
using PKHeX.Facade;
using PKHeX.Facade.Repositories;

namespace PKHeX.Godot.Scripts.CurrentPokemon;

public partial class HeldItemMenuButton : MenuButton
{
    private Application _application = null!;

    public override void _Ready()
    {
        _application = GetNode<Application>(Application.NodePath);
        _application.CurrentPokemonChanged += CurrentPokemonChanged;
        _application.FileLoaded += OnFileLoaded;
    }

    private void CurrentPokemonChanged()
    {
        if (_application.CurrentPokemon is null || _application.CurrentPokemon.HeldItem.IsNone)
        {
            Disabled = true;
            Text = " ";
        }
        else
        {
            Disabled = false;
            Text = _application.CurrentPokemon.HeldItem.Name;
        }
    }

    private void OnFileLoaded()
    {
        Text = " ";

        var popup = GetPopup();
        popup.Clear(freeSubmenus: true);

        if (_application.Game is null || !_application.Game.ItemRepository.GameItems.Any())
            return;

        foreach (var gameItem in _application.Game.ItemRepository.GameItems)
        {
            popup.AddItem(gameItem.Name);
        }
    }
}
