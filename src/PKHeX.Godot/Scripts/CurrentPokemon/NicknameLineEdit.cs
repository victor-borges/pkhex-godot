namespace PKHeX.Godot.Scripts.CurrentPokemon;

public partial class NicknameLineEdit : LineEdit
{
    private Application _application = null!;

    public override void _Ready()
    {
        _application = GetNode<Application>(Application.NodePath);
        _application.CurrentPokemonChanged += CurrentPokemonChanged;

        TextChanged += OnTextChanged;
    }

    private void OnTextChanged(string text)
    {
        if (_application.Game is null || _application.CurrentPokemon is null)
            return;

        _application.CurrentPokemon.Pkm.SetNickname(text);
        _application.TriggerCurrentPokemonChanged();
    }

    private void CurrentPokemonChanged()
    {
        if (_application.Game is null || _application.CurrentPokemon is null)
        {
            Editable = false;
        }
        else
        {
            var isNicknamed = _application.CurrentPokemon.Pkm.IsNicknamed;

            Text = isNicknamed ? _application.CurrentPokemon.Pkm.Nickname : string.Empty;
            Editable = isNicknamed;
        }
    }
}
