using PKHeX.Godot.Extensions;

namespace PKHeX.Godot.Scripts.CurrentPokemon.Controls;

public partial class FormOptionButton : OptionButton
{
    private Application _application = null!;

    public override void _Ready()
    {
        _application = GetNode<Application>(Application.NodePath);
        _application.CurrentPokemonChanged += CurrentPokemonChanged;

        ItemSelected += OnFormSelected;
        PopulateFormsMenu();
    }

    private void CurrentPokemonChanged()
    {
        PopulateFormsMenu();
    }

    private void OnFormSelected(long index)
    {
        if (_application.CurrentPokemon is null || !_application.CurrentPokemon.PersonalInfo.HasForms)
            return;

        var id = GetItemId((int)index);
        _application.CurrentPokemon.Form = (byte)id;
        _application.EmitEventCurrentPokemonChanged();
    }

    private void PopulateFormsMenu()
    {
        Clear();

        if (_application.Game is null || _application.CurrentPokemon is null || !_application.CurrentPokemon.PersonalInfo.HasForms)
        {
            Selected = -1;
            Disabled = true;
            return;
        }

        var forms = _application.CurrentPokemon.Forms.ToArray();

        if (forms.Length is 0 || forms is [{ Value: 0 }])
        {
            Disabled = true;
        }
        else
        {
            foreach (var formDefinition in forms)
            {
                AddItem(formDefinition.Text, formDefinition.Value);

                if (formDefinition.Value == _application.CurrentPokemon.Form)
                    Selected = GetItemIndex(formDefinition.Value);
            }
        }

        if (ItemCount > 0)
            Disabled = false;
    }
}
