namespace PKHeX.Godot.Scripts.CurrentPokemon;

public partial class NicknameCheckBox : CheckBox
{
    private Application _application = null!;

    public override void _Ready()
    {
        _application = GetNode<Application>(Application.NodePath);
        _application.CurrentPokemonChanged += CurrentPokemonChanged;

        Toggled += OnToggled;
    }

    private void OnToggled(bool toggledOn)
    {
        if (_application.CurrentPokemon is null)
            return;

        if (toggledOn)
        {
            _application.CurrentPokemon?.Pkm.IsNicknamed = true;
        }
        else
        {
            _application.CurrentPokemon?.Pkm.ClearNickname();
        }

        _application.EmitEventCurrentPokemonChanged();
    }

    private void CurrentPokemonChanged()
    {
        if (_application.Game is null || _application.CurrentPokemon is null)
        {
            Disabled = true;
            SetPressedNoSignal(false);
        }
        else
        {
            Disabled = false;
            var isNicknamed = _application.CurrentPokemon.Pkm.IsNicknamed;
            SetPressedNoSignal(isNicknamed);
        }
    }
}
