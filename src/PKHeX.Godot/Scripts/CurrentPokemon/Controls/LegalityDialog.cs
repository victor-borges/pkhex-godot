using PKHeX.Facade.Extensions;

namespace PKHeX.Godot.Scripts.CurrentPokemon.Controls;

public partial class LegalityDialog : AcceptDialog
{
    private Application _application = null!;
    private FoldableContainer _foldableContainer = null!;
    private Label _simpleReportLabel = null!;
    private RichTextLabel _fullReportLabel = null!;
    private Button? _copyToClipboardButton;

    public override void _Ready()
    {
        _application = GetNode<Application>(Application.NodePath);
        _simpleReportLabel = GetNode<Label>("%SimpleReportLabel");
        _fullReportLabel = GetNode<RichTextLabel>("%FullReportLabel");
        _foldableContainer = GetNode<FoldableContainer>("%FoldableContainer");

        _copyToClipboardButton ??= AddButton("Copy to Clipboard");
        _copyToClipboardButton.Pressed += OnCopyToClipboardButtonPressed;
    }

    private void OnButtonPressed()
    {
        if (_application.CurrentPokemon is null)
            return;

        _foldableContainer.Folded = true;

        var legalityAnalysis = _application.CurrentPokemon.Legality();
        var localizer = LegalityLocalizationContext.Create(legalityAnalysis, GameInfo.CurrentLanguage);
        var simpleReport = localizer.Report(false);
        var fullReport = localizer.Report(true);

        var intro = simpleReport + System.Environment.NewLine;

        if (legalityAnalysis.Valid)
            intro += System.Environment.NewLine;

        fullReport = fullReport.Replace(intro, "");
        fullReport = fullReport.Replace("===\n", "");

        _simpleReportLabel.Text = simpleReport;
        _fullReportLabel.Text = fullReport;

        ChildControlsChanged();
        PopupCentered();
    }

    private void OnCopyToClipboardButtonPressed()
    {
        if (_application.CurrentPokemon is null)
            return;

        var legalityAnalysis = _application.CurrentPokemon.Legality();
        var localizer = LegalityLocalizationContext.Create(legalityAnalysis, GameInfo.CurrentLanguage);
        var fullReport = localizer.Report(true);
        fullReport = fullReport.Replace("===\n", "");

        var encounterTextLines = _application.CurrentPokemon.Legality().EncounterOriginal.GetTextLines();
        var clipboardContent = fullReport + System.Environment.NewLine + System.Environment.NewLine +
                  string.Join(System.Environment.NewLine, encounterTextLines);

        DisplayServer.ClipboardSet(clipboardContent);
        Hide();
    }
}
