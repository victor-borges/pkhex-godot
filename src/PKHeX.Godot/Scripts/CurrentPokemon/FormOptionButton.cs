using System.Linq;
using PKHeX.Facade.Repositories;

namespace PKHeX.Godot.Scripts.CurrentPokemon;

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

    private void OnFormSelected(long id)
    {
        if (_application.CurrentPokemon is null || !_application.CurrentPokemon.Form.HasForm)
            return;

        _application.CurrentPokemon.Pkm.Form = (byte)id;
        _application.EmitEventCurrentPokemonChanged();
    }

    private void PopulateFormsMenu()
    {
        Clear();

        if (_application.Game is null || _application.CurrentPokemon is null || !_application.CurrentPokemon.Form.HasForm)
        {
            Disabled = true;
            AddItem(" ");
            return;
        }

        var forms = FormRepository.GetFor(_application.CurrentPokemon).ToArray();

        if (forms.Length is 0 || forms is [{ ByteId: 0 }])
        {
            AddItem(" ");
        }
        else
        {
            foreach (var formDefinition in forms)
            {
                var byteId = formDefinition.ByteId;
                AddItem(formDefinition.Name, byteId);

                if (byteId == _application.CurrentPokemon.Form.Form.ByteId)
                    Selected = byteId;
            }
        }

        if (ItemCount > 0)
            Disabled = false;
    }
}
