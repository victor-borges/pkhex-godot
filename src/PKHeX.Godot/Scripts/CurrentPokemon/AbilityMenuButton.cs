using System;
using Godot;
using PKHeX.Core;

namespace PKHeX.Godot.Scripts.CurrentPokemon;

public partial class AbilityMenuButton : MenuButton
{
    private Application _application = null!;
    private LineEdit _abilityLineEdit = null!;

    public override void _Ready()
    {
        _application = GetNode<Application>(Application.NodePath);
        _abilityLineEdit = GetNode<LineEdit>("%AbilityLineEdit");

        _application.CurrentPokemonChanged += CurrentPokemonChanged;
    }

    private void CurrentPokemonChanged()
    {
        var popup = GetPopup();
        popup.Clear();

        if (_application.Game is null || _application.CurrentPokemon is null || _application.CurrentPokemon.Pkm.Format < 3)
        {
            Disabled = true;
            _abilityLineEdit.Text = " ";
            Text = " ";
        }
        else
        {
            Disabled = false;
            var pi = _application.CurrentPokemon.Pkm.PersonalInfo;
            var abilities = GameInfo.FilteredSources.GetAbilityList(pi);

            foreach (var ability in abilities)
                popup.AddItem(ability.Text);

            var abilityId = _application.CurrentPokemon.Ability.Id;
            var abilityIndex = pi.GetIndexOfAbility(abilityId);

            _abilityLineEdit.Text = Math.Clamp(abilityIndex, 0, abilities.Count - 1) switch
            {
                2 => "[H]",
                var i => $"[{i}]"
            };

            Text = _application.CurrentPokemon.Ability.Name;
        }
    }
}
