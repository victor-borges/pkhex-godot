using Godot;
using PKHeX.Core;

namespace PKHeX.Godot.Scripts.CurrentPokemon;

public partial class NicknameLineEdit : LineEdit
{
    private Application _application = null!;

    public override void _Ready()
    {
        _application = GetNode<Application>(Application.NodePath);
        _application.CurrentPokemonChanged += CurrentPokemonChanged;
        _application.FileLoaded += OnFileLoaded;

        TextChanged += OnTextChanged;
    }

    private void OnTextChanged(string text)
    {
        if (_application.Game is null || _application.CurrentPokemon is null)
            return;

        _application.CurrentPokemon.Pkm.SetNickname(text);
    }

    private void CurrentPokemonChanged()
    {
        if (_application.Game is null || _application.CurrentPokemon is null)
            return;

        var isNicknamed = _application.CurrentPokemon.Pkm.IsNicknamed;

        Text = isNicknamed ? _application.CurrentPokemon.Pkm.Nickname : string.Empty;
        Editable = isNicknamed;
    }

    private void OnFileLoaded()
    {
        Text = string.Empty;
        Editable = false;
    }
}
