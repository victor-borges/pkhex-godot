using Godot;
using PKHeX.Core;

namespace PKHeX.Godot.Scripts.CurrentPokemon;

public partial class AbilityMenuButton : MenuButton
{
    private SignalBus _signalBus = null!;
    private GameData _gameData = null!;
    private LineEdit _abilityLineEdit = null!;

    public override void _Ready()
    {
        _gameData = GetNode<GameData>("/root/GameData");
        _signalBus = GetNode<SignalBus>("/root/SignalBus");
        _abilityLineEdit = GetNode<LineEdit>("%AbilityLineEdit");

        _signalBus.CurrentPokemonChanged += CurrentPokemonChanged;
        _signalBus.FileLoaded += OnFileLoaded;
    }

    private void CurrentPokemonChanged()
    {
        var popup = GetPopup();
        popup.Clear();

        if (_gameData.Game is null || _gameData.CurrentPokemon is null || _gameData.CurrentPokemon.Pkm.Format < 3)
        {
            _abilityLineEdit.Text = " ";
            Text = " ";
        }
        else
        {
            var pi = _gameData.CurrentPokemon.Pkm.PersonalInfo;
            var abilities = GameInfo.FilteredSources.GetAbilityList(pi);

            foreach (var ability in abilities)
                popup.AddItem(ability.Text);

            _abilityLineEdit.Text = pi.GetIndexOfAbility(_gameData.CurrentPokemon.Ability.Id) switch
            {
                0 => "[1]",
                1 => "[2]",
                2 => "[H]",
                3 => "[2]",
                _ => " ",
            };

            Text = _gameData.CurrentPokemon.Ability.Name;
        }
    }

    private void OnFileLoaded(string _)
    {
        var popup = GetPopup();
        popup.Clear();
        _abilityLineEdit.Text = " ";
        Text = " ";
    }
}
