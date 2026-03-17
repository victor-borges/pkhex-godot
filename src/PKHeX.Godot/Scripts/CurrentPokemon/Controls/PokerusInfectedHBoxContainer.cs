namespace PKHeX.Godot.Scripts.CurrentPokemon.Controls;

public partial class PokerusInfectedHBoxContainer : HBoxContainer
{
    public override void _Ready()
    {
        Application.Instance.CurrentPokemonChanged += CurrentPokemonChanged;
    }

    private void CurrentPokemonChanged()
    {
        if (Application.SaveFile is null || Application.CurrentPokemon is null)
        {
            Visible = false;
        }
        else
        {
            Visible = Application.CurrentPokemon.IsPokerusInfected;
        }
    }
}
