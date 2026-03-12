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

        public IEnumerable<ComboItem> Forms
        {
            get
            {
                try
                {
                    return FormConverter.GetFormList(pkm.Species, GameInfo.Strings.types, GameInfo.Strings.forms, GameInfo.GenderSymbolUnicode, pkm.Context)
                        .Select((form, index) => new ComboItem(form, index));
                }
                catch (ArgumentOutOfRangeException)
                {
                    return [];
                }
            }
        }
    }
}
