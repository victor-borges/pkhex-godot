namespace PKHeX.Godot.Scripts.CurrentPokemon.Controls;

public partial class FriendshipSpinBox : SpinBox
{
    private Label _friendshipLabel = null!;

    public override void _Ready()
    {
        _friendshipLabel = GetNode<Label>("../FriendshipLabel");
        Application.Instance.CurrentPokemonChanged += CurrentPokemonChanged;

        ValueChanged += OnValueChanged;
    }

    private void OnValueChanged(double value)
    {
        if (Application.CurrentPokemon is null)
            return;

        Application.CurrentPokemon.CurrentFriendship = (byte)value;
        Application.Instance.EmitEventCurrentPokemonChanged();
    }

    private void CurrentPokemonChanged()
    {
        if (Application.SaveFile is null || Application.CurrentPokemon is null)
        {
            Editable = false;
            SetValueNoSignal(0);
        }
        else
        {
            Editable = true;
            SetValueNoSignal(Application.CurrentPokemon.CurrentFriendship);
            _friendshipLabel.Text = Application.CurrentPokemon.IsEgg ? "Hatch Counter:" : "Friendship:";
        }
    }
}
