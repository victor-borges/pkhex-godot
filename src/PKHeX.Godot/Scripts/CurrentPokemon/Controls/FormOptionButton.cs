using PKHeX.Godot.Extensions;

namespace PKHeX.Godot.Scripts.CurrentPokemon.Controls;

public partial class FormOptionButton : OptionButton
{
    public override void _Ready()
    {
        Application.Instance.CurrentPokemonChanged += CurrentPokemonChanged;

        ItemSelected += OnFormSelected;
        PopulateFormsMenu();
    }

    private void CurrentPokemonChanged()
    {
        PopulateFormsMenu();
    }

    private void OnFormSelected(long index)
    {
        if (Application.CurrentPokemon is null || !Application.CurrentPokemon.PersonalInfo.HasForms)
            return;

        var id = GetItemId((int)index);
        Application.CurrentPokemon.Form = (byte)id;
        Application.Instance.EmitEventCurrentPokemonChanged();
    }

    private void PopulateFormsMenu()
    {
        Clear();

        if (Application.SaveFile is null || Application.CurrentPokemon is null || !Application.CurrentPokemon.PersonalInfo.HasForms)
        {
            Selected = -1;
            Disabled = true;
            return;
        }

        var forms = Application.CurrentPokemon.Forms.ToArray();

        if (forms.Length is 0 || forms is [{ Value: 0 }])
        {
            Disabled = true;
        }
        else
        {
            foreach (var formDefinition in forms)
            {
                AddItem(formDefinition.Text, formDefinition.Value);

                if (formDefinition.Value == Application.CurrentPokemon.Form)
                    Selected = GetItemIndex(formDefinition.Value);
            }
        }

        if (ItemCount > 0)
            Disabled = false;
    }
}
