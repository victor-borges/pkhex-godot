using System.Linq;
using Godot;
using PKHeX.Facade.Repositories;

namespace PKHeX.Godot.Scripts.CurrentPokemon;

public partial class FormMenuButton : MenuButton
{
    private Application _application = null!;

    public override void _Ready()
    {
        _application = GetNode<Application>(Application.NodePath);
        _application.CurrentPokemonChanged += CurrentPokemonChanged;
        _application.FileLoaded += OnFileLoaded;

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

        if (_application.Game is null || _application.CurrentPokemon is null || !_application.CurrentPokemon.Form.HasForm)
        {
            Text = " ";
            return;
        }

        var forms = FormRepository.GetFor(_application.CurrentPokemon).ToArray();

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

            Text = _application.CurrentPokemon.Form.Form.Name;
        }
    }

    private void OnFileLoaded()
    {
        Text = " ";
    }
}
