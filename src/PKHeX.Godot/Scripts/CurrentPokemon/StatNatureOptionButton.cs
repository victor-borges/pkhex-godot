namespace PKHeX.Godot.Scripts.CurrentPokemon;

public partial class StatNatureOptionButton : OptionButton
{
    private Application _application = null!;

    public override void _Ready()
    {
        _application = GetNode<Application>(Application.NodePath);
        _application.CurrentPokemonChanged += CurrentPokemonChanged;

        ItemSelected += OnNatureSelected;

        Clear();

        var i = 0;
        foreach (var nature in GameInfo.Strings.Natures)
        {
            AddItem(nature, i++);
        }
    }

    private void OnNatureSelected(long id)
    {
        if (id >= (long)Nature.Random || _application.CurrentPokemon is null)
            return;

        _application.CurrentPokemon.Pkm.StatNature = (Nature)id;
        _application.EmitEventCurrentPokemonChanged();
    }

    private void CurrentPokemonChanged()
    {
        if (_application.Game is null || _application.CurrentPokemon is null)
        {
            Disabled = true;
        }
        else
        {
            Disabled = false;
            Selected = (int)_application.CurrentPokemon.Natures.StatNature;
        }
    }
}
