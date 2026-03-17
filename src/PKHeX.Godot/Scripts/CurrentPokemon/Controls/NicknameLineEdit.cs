namespace PKHeX.Godot.Scripts.CurrentPokemon.Controls;

public partial class NicknameLineEdit : LineEdit
{
        private Label _lengthLabel = null!;

    public override void _Ready()
    {
        Application.Instance.CurrentPokemonChanged += CurrentPokemonChanged;
        Application.Instance.FileLoaded += OnFileLoaded;

        _lengthLabel = GetNode<Label>("NicknameLengthLabel");

        TextChanged += OnTextChanged;
        TextSubmitted += OnTextSubmitted;
        FocusExited += OnFocusExited;

        if (Application.SaveFile is not null)
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
        if (Application.SaveFile is null || Application.CurrentPokemon is null)
            return;

        Application.CurrentPokemon.SetNickname(text);
        _lengthLabel.Visible = Application.CurrentPokemon.IsNicknamed;

        Application.Instance.EmitEventCurrentPokemonChanged();
    }

    private void CurrentPokemonChanged()
    {
        Clear();

        if (Application.SaveFile is null || Application.CurrentPokemon is null)
        {
            Editable = false;
            _lengthLabel.Visible = false;
        }
        else
        {
            var isNicknamed = Application.CurrentPokemon.IsNicknamed;
            var nicknameLength = Application.CurrentPokemon.Nickname.Length;

            Editable = isNicknamed;
            Text = Application.CurrentPokemon.Nickname;
            _lengthLabel.Text = $"{nicknameLength}/{MaxLength}";
            _lengthLabel.Visible = true;
        }
    }

    private void OnFileLoaded()
    {
        Clear();

        if (Application.SaveFile is null)
            return;

        if ((LanguageID)Application.SaveFile.Language is LanguageID.Japanese or LanguageID.Korean)
        {
            SetMaxLength(5);
        }
        else
        {
            SetMaxLength(Application.SaveFile.Generation <= 5 ? 10 : 12);
        }
    }
}
