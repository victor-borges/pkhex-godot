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

        _application.CurrentPokemon.SetNickname(text);
        _lengthLabel.Visible = _application.CurrentPokemon.IsNicknamed;

        _application.EmitEventCurrentPokemonChanged();
    }

    private void CurrentPokemonChanged()
    {
        Clear();

        if (_application.Game is null || _application.CurrentPokemon is null)
        {
            Editable = false;
            _lengthLabel.Visible = false;
        }
        else
        {
            var isNicknamed = _application.CurrentPokemon.IsNicknamed;
            var nicknameLength = _application.CurrentPokemon.Nickname.Length;

            Editable = isNicknamed;
            Text = _application.CurrentPokemon.Nickname;
            _lengthLabel.Text = $"{nicknameLength}/{MaxLength}";
            _lengthLabel.Visible = true;
        }
    }

    private void OnFileLoaded()
    {
        Clear();

        if (_application.Game is null)
            return;

        if ((LanguageID)_application.Game.Language is LanguageID.Japanese or LanguageID.Korean)
        {
            SetMaxLength(5);
        }
        else
        {
            SetMaxLength(_application.Game.Generation <= 5 ? 10 : 12);
        }
    }
}
