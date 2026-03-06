using Godot;

namespace PKHeX.Godot.Scripts.CurrentPokemon;

public partial class NicknameCheckBox : CheckBox
{
    private Application _application = null!;
    private LineEdit _nicknameLineEdit = null!;

    public override void _Ready()
    {
        _application = GetNode<Application>("/root/Application");
        _nicknameLineEdit = GetNode<LineEdit>("%NicknameLineEdit");

        _application.CurrentPokemonChanged += CurrentPokemonChanged;
        _application.FileLoaded += OnFileLoaded;
    }

    private void CurrentPokemonChanged()
    {
        if (_application.Game is null || _application.CurrentPokemon is null)
        {
            ButtonPressed = false;
        }
        else
        {
            var isNicknamed = _application.CurrentPokemon.Pkm.IsNicknamed;

            ButtonPressed = isNicknamed;
        }
    }

    private void OnFileLoaded()
    {
        ButtonPressed = false;
    }
}
