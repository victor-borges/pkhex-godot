namespace PKHeX.Godot.Scripts.CurrentPokemon.Controls;

public partial class AbilityOptionButton : OptionButton
{
    private Application _application = null!;

    public override void _Ready()
    {
        _application = GetNode<Application>(Application.NodePath);
        _application.CurrentPokemonChanged += CurrentPokemonChanged;

        ItemSelected += OnAbilitySelected;
    }

    private void OnAbilitySelected(long index)
    {
        if (_application.CurrentPokemon is null)
            return;

        var abilityId = GetItemId((int)index);
        var abilityIndex = _application.CurrentPokemon.Pkm.PersonalInfo.GetIndexOfAbility(abilityId);
        if (abilityIndex == -1) return;

        _application.CurrentPokemon.Pkm.RefreshAbility(abilityIndex);
        _application.EmitEventCurrentPokemonChanged();
    }

    private void CurrentPokemonChanged()
    {
        Clear();

        if (_application.Game is null || _application.CurrentPokemon is null || _application.CurrentPokemon.Pkm.Format < 3)
        {
            Disabled = true;
        }
        else
        {
            var pi = _application.CurrentPokemon.Pkm.PersonalInfo;
            var abilities = GameInfo.FilteredSources.GetAbilityList(pi);

            foreach (var ability in abilities)
            {
                AddItem(ability.Text, ability.Value);

                if (_application.CurrentPokemon.Pkm.Ability == ability.Value)
                    Selected = GetItemIndex(ability.Value);
            }

            Disabled = ItemCount == 0;
        }
    }
}
