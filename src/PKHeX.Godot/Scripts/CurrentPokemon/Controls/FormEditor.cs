namespace PKHeX.Godot.Scripts.CurrentPokemon.Controls;

public partial class FormEditor : HBoxContainer
{
	public override void _Ready()
	{
		Application.Instance.CurrentPokemonChanged += CurrentPokemonChanged;
	}

	private void CurrentPokemonChanged()
	{
		if (Application.SaveFile is null || Application.CurrentPokemon is null)
			return;

		Visible = Application.CurrentPokemon.PersonalInfo.HasForms;
	}
}
