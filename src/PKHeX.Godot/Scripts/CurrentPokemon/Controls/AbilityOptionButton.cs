namespace PKHeX.Godot.Scripts.CurrentPokemon.Controls;

public partial class AbilityOptionButton : OptionButton
{
    public override void _Ready()
    {
        Application.Instance.CurrentPokemonChanged += CurrentPokemonChanged;
        ItemSelected += OnAbilitySelected;
    }

    private void OnAbilitySelected(long index)
    {
        if (Application.CurrentPokemon is null)
            return;

        var abilityId = GetItemId((int)index);
        var abilityIndex = Application.CurrentPokemon.PersonalInfo.GetIndexOfAbility(abilityId);
        if (abilityIndex == -1) return;

        Application.CurrentPokemon.RefreshAbility(abilityIndex);
        Application.Instance.EmitEventCurrentPokemonChanged();
    }

    private void CurrentPokemonChanged()
    {
        Clear();

        if (Application.SaveFile is null || Application.CurrentPokemon is null || Application.CurrentPokemon.Format < 3)
        {
            Disabled = true;
        }
        else
        {
            var pi = Application.CurrentPokemon.PersonalInfo;
            var abilities = GameInfo.FilteredSources.GetAbilityList(pi);

            foreach (var ability in abilities)
            {
                AddItem(ability.Text, ability.Value);

                if (Application.CurrentPokemon.Ability == ability.Value)
                    Selected = GetItemIndex(ability.Value);
            }

            Disabled = ItemCount == 0;
        }
    }
}
