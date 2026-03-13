namespace PKHeX.Godot.Scripts.CurrentPokemon.Controls;

public partial class FormEditor : HBoxContainer
{
	private Application _application = null!;

	public override void _Ready()
	{
		_application = GetNode<Application>(Application.NodePath);
		_application.CurrentPokemonChanged += CurrentPokemonChanged;
	}

	private void CurrentPokemonChanged()
	{
		if (_application.Game is null || _application.CurrentPokemon is null)
			return;

		Visible = _application.CurrentPokemon.PersonalInfo.HasForms;
	}
}
