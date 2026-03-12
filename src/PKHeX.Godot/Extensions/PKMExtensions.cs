namespace PKHeX.Godot.Extensions;

public static class PKMExtensions
{
    extension(PKM pkm)
    {
        public LegalityAnalysis Legality => new(pkm);

        public Shiny ShinyType => ShinyExtensions.GetType(pkm);

        public string FormName => FormConverter.GetStringFromForm(pkm.Species, pkm.Form, GameInfo.Strings,
            GameInfo.GenderSymbolUnicode, pkm.Context);

        public void RerollPID()
        {
            var isShiny = pkm.IsShiny;
            pkm.SetPIDGender(pkm.Gender);
            pkm.SetIsShiny(isShiny);
        }

        public IEnumerable<FormDefinition> Forms
        {
            get
            {
                try
                {
                    return FormConverter.GetFormList(pkm.Species, GameInfo.Strings.types, GameInfo.Strings.forms, GameInfo.GenderSymbolUnicode, pkm.Context)
                        .Select((form, index) => new FormDefinition((ushort)index, form));
                }
                catch (ArgumentOutOfRangeException)
                {
                    return [];
                }
            }
        }
    }
}

public record FormDefinition(ushort Id, string Name)
{
    public byte ByteId => (byte)Id;

    public bool IsNormal => Name.Equals("Normal", StringComparison.InvariantCultureIgnoreCase);
    public bool IsAlolan => Name.Equals("Alola", StringComparison.InvariantCultureIgnoreCase);
    public bool IsGalarian => Name.Equals("Galar", StringComparison.InvariantCultureIgnoreCase);
    public bool IsHisuian => Name.Equals("Hisui", StringComparison.InvariantCultureIgnoreCase);

    public static readonly FormDefinition Default = new(0, string.Empty);
}
