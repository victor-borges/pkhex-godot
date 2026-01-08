using System.Linq;
using Godot;
using PKHeX.Facade.Repositories;

namespace PKHeX.Godot.Scripts.CurrentPokemon;

public partial class FormMenuButton : MenuButton
{
    private SignalBus _signalBus = null!;
    private GameData _gameData = null!;

    public override void _Ready()
    {
        _gameData = GetNode<GameData>("/root/GameData");
        _signalBus = GetNode<SignalBus>("/root/SignalBus");

        _signalBus.CurrentPokemonChanged += CurrentPokemonChanged;
        _signalBus.FileLoaded += OnFileLoaded;

        PopulateFormsMenu();
    }

    private void CurrentPokemonChanged()
    {
        PopulateFormsMenu();
    }

    private void PopulateFormsMenu()
    {
        var popup = GetPopup();
        popup.Clear(freeSubmenus: true);

        if (_gameData.Game is null || _gameData.CurrentPokemon is null || !_gameData.CurrentPokemon.Form.HasForm)
        {
            Text = " ";
            return;
        }

        var forms = FormRepository.GetFor(_gameData.CurrentPokemon).ToArray();

        if (forms.Length is 0 || forms is [{ ByteId: 0 }])
        {
            Text = " ";
        }
        else
        {
            foreach (var formDefinition in forms)
            {
                popup.AddItem(formDefinition.Name);
            }

            Text = _gameData.CurrentPokemon.Form.Form.Name;
        }
    }

    private void OnFileLoaded(string _)
    {
        Text = " ";
    }
}
