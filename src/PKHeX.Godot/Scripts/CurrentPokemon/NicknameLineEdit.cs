using Godot;

namespace PKHeX.Godot.Scripts.CurrentPokemon;

public partial class NicknameLineEdit : LineEdit
{
    private Application _application = null!;

    public override void _Ready()
    {
        _application = GetNode<Application>("/root/Application");

        _application.CurrentPokemonChanged += CurrentPokemonChanged;
        _application.FileLoaded += OnFileLoaded;
    }

    private void CurrentPokemonChanged()
    {
        if (_application.Game is null || _application.CurrentPokemon is null)
        {
            Text = string.Empty;
        }
        else
        {
            var isNicknamed = _application.CurrentPokemon.Pkm.IsNicknamed;
            Text = isNicknamed ? _application.CurrentPokemon.Nickname : string.Empty;
        }
    }

    private void OnFileLoaded()
    {
        Text = string.Empty;
    }
}
