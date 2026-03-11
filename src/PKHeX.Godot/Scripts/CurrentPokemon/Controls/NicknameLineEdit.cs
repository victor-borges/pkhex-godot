namespace PKHeX.Godot.Scripts.CurrentPokemon.Controls;

public partial class NicknameLineEdit : LineEdit
{
    private Application _application = null!;
    private Label _lengthLabel = null!;

    public override void _Ready()
    {
        _application = GetNode<Application>(Application.NodePath);
        _application.CurrentPokemonChanged += CurrentPokemonChanged;
        _application.FileLoaded += OnFileLoaded;

        _lengthLabel = GetNode<Label>("NicknameLengthLabel");

        TextChanged += OnTextChanged;
        TextSubmitted += OnTextSubmitted;
        FocusExited += OnFocusExited;

        if (_application.Game is not null)
            OnFileLoaded();
    }

    private void OnTextChanged(string text)
    {
        _lengthLabel.Text = $"{text.Length}/{MaxLength}";
    }

    private void OnTextSubmitted(string text)
    {
        SetNickname(text);
    }

    private void OnFocusExited()
    {
        SetNickname(Text);
    }

    private void SetNickname(string text)
    {
        if (_application.Game is null || _application.CurrentPokemon is null)
            return;

        _application.CurrentPokemon.Pkm.SetNickname(text);
        _lengthLabel.Visible = _application.CurrentPokemon.Pkm.IsNicknamed;

        _application.EmitEventCurrentPokemonChanged();
    }

    private void CurrentPokemonChanged()
    {
        if (_application.Game is null || _application.CurrentPokemon is null)
        {
            Editable = false;
            _lengthLabel.Visible = false;
        }
        else
        {
            var isNicknamed = _application.CurrentPokemon.Pkm.IsNicknamed;
            var nicknameLength = isNicknamed ? _application.CurrentPokemon.Pkm.Nickname.Length : 0;

            Text = isNicknamed ? _application.CurrentPokemon.Pkm.Nickname : string.Empty;
            _lengthLabel.Text = $"{nicknameLength}/{MaxLength}";

            Editable = isNicknamed;
            _lengthLabel.Visible = isNicknamed;
        }
    }

    private void OnFileLoaded()
    {
        if (_application.Game is null)
            return;

        SetMaxLength(_application.Game.SaveFile.Generation <= 5 ? 10 : 12);
    }
}
