using PKHeX.Godot.Extensions;

namespace PKHeX.Godot.Scripts.CurrentPokemon.Controls;

public partial class GenderButton : Button
{
    private Application _application = null!;

    public override void _Ready()
    {
        _application = GetNode<Application>(Application.NodePath);
        _application.CurrentPokemonChanged += CurrentPokemonChanged;

        Pressed += OnButtonPressed;
    }

    private void OnButtonPressed()
    {
        if (_application.CurrentPokemon is null)
            return;

        var pkm = _application.CurrentPokemon;
        if (!pkm.PersonalInfo.IsDualGender)
        {
            Disabled = true;
            var expect = pkm.PersonalInfo.FixedGender();
            SetGenderIcon(expect);
        }
        else
        {
            var gender = pkm.Gender;
            var canToggle = gender < 2;

            if (!canToggle)
                pkm.Gender = 0; // fix bad genders

            if (pkm.Format <= 2)
            {
                //Stats.SetATKIVGender(gender);
            }
            else if (pkm.Format <= 4)
            {
                pkm.SetPIDGender(gender);
            }
            else
            {
                pkm.Gender = (byte)((gender + 1) % 2);
            }

            if (EntityGender.GetFromString(_application.CurrentPokemon.FormName) < 2) // Gendered Forms
            {
                var formCount = _application.CurrentPokemon.Forms.Count();
                pkm.Form = (byte)Math.Min(gender, formCount - 1);
            }
        }

        _application.EmitEventCurrentPokemonChanged();
    }

    private void CurrentPokemonChanged()
    {
        if (_application.CurrentPokemon is null)
        {
            Disabled = true;
            SetGenderIcon((byte)Gender.Genderless);
            return;
        }

        var pkm = _application.CurrentPokemon;
        if (!pkm.PersonalInfo.IsDualGender)
        {
            Disabled = true;
            SetGenderIcon(pkm.PersonalInfo.FixedGender());
        }
        else
        {
            Disabled = false;
            var genderIndex = _application.CurrentPokemon.Gender;
            SetGenderIcon(genderIndex);
        }
    }

    private void SetGenderIcon(byte genderIndex) =>
        Icon = GD.Load<Texture2D>(Assets.Sprites.Gender(genderIndex));
}
