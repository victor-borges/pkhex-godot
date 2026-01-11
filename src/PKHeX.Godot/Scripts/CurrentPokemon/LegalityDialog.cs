using Godot;
using PKHeX.Core;
using PKHeX.Facade.Extensions;

namespace PKHeX.Godot.Scripts.CurrentPokemon;

public partial class LegalityDialog : AcceptDialog
{
    private SignalBus _signalBus = null!;
    private GameData _gameData = null!;
    private FoldableContainer _foldableContainer = null!;
    private Label _simpleReportLabel = null!;
    private Label _fullReportLabel = null!;
    private Button? _copyToClipboardButton;

    public override void _Ready()
    {
        _gameData = GetNode<GameData>("/root/GameData");
        _signalBus = GetNode<SignalBus>("/root/SignalBus");
        _simpleReportLabel = GetNode<Label>("%SimpleReportLabel");
        _fullReportLabel = GetNode<Label>("%FullReportLabel");
        _foldableContainer = GetNode<FoldableContainer>("%FoldableContainer");

        _copyToClipboardButton ??= AddButton("Copy to Clipboard");
        _copyToClipboardButton.Pressed += OnCopyToClipboardButtonPressed;
    }

    private void OnButtonPressed()
    {
        if (_gameData.CurrentPokemon is null)
            return;

        _foldableContainer.Folded = true;

        var legalityAnalysis = _gameData.CurrentPokemon.Legality();
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

        PopupCentered();
    }

    private void OnCopyToClipboardButtonPressed()
    {
        if (_gameData.CurrentPokemon is null)
            return;

        var legalityAnalysis = _gameData.CurrentPokemon.Legality();
        var localizer = LegalityLocalizationContext.Create(legalityAnalysis, GameInfo.CurrentLanguage);
        var fullReport = localizer.Report(true);
        fullReport = fullReport.Replace("===\n", "");

        var encounterTextLines = _gameData.CurrentPokemon.Legality().EncounterOriginal.GetTextLines();
        var clipboardContent = fullReport + System.Environment.NewLine + System.Environment.NewLine +
                  string.Join(System.Environment.NewLine, encounterTextLines);

        DisplayServer.ClipboardSet(clipboardContent);
    }
}
