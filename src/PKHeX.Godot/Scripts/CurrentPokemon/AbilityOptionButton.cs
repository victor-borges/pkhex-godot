namespace PKHeX.Godot.Scripts.CurrentPokemon;

public partial class AbilityOptionButton : OptionButton
{
    private Application _application = null!;

    public override void _Ready()
    {
        _application = GetNode<Application>(Application.NodePath);
        _application.CurrentPokemonChanged += CurrentPokemonChanged;

        ItemSelected += OnAbilitySelected;
    }

    private void OnAbilitySelected(long id)
    {
        if (_application.CurrentPokemon is null)
            return;

        var index = _application.CurrentPokemon.Pkm.PersonalInfo.GetIndexOfAbility((int)id);
        if (index == -1) return;

        _application.CurrentPokemon.Pkm.RefreshAbility(index);
        _application.EmitEventCurrentPokemonChanged();
    }

    private void CurrentPokemonChanged()
    {
        Clear();

        if (_application.Game is null || _application.CurrentPokemon is null || _application.CurrentPokemon.Pkm.Format < 3)
        {
            Disabled = true;
            AddItem(" ");
        }
        else
        {
            var pi = _application.CurrentPokemon.Pkm.PersonalInfo;
            var abilities = GameInfo.FilteredSources.GetAbilityList(pi);

            foreach (var ability in abilities)
            {
                AddItem(ability.Text, ability.Value);

                if (_application.CurrentPokemon.Pkm.Ability == ability.Value)
                    Selected = ability.Value;
            }

            if (ItemCount > 0)
                Disabled = false;

            // var abilityId = _application.CurrentPokemon.Ability.Id;
            // var abilityIndex = Math.Clamp(pi.GetIndexOfAbility(abilityId) + 1, 1, abilities.Count);
            // _abilityLineEdit.Text = $"[{(abilityIndex == 3 ? "H" : abilityIndex)}]";
        }
    }
}
