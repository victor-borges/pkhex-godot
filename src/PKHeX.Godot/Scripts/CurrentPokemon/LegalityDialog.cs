using Godot;
using PKHeX.Core;
using PKHeX.Facade.Extensions;

namespace PKHeX.Godot.Scripts.CurrentPokemon;

public partial class LegalityDialog : AcceptDialog
{
    private SignalBus _signalBus = null!;
    private GameData _gameData = null!;
    private Button? _fullReportButton;

    public override void _Ready()
    {
        _gameData = GetNode<GameData>("/root/GameData");
        _signalBus = GetNode<SignalBus>("/root/SignalBus");
        _fullReportButton ??= AddButton("Full Report", action: "FullReport");

        CustomAction += OnFullReport;
    }

    private void OnButtonPressed()
    {
        if (_gameData.CurrentPokemon is null)
            return;

        var localizer = LegalityLocalizationContext.Create(_gameData.CurrentPokemon.Legality(), GameInfo.CurrentLanguage);
        var simpleReport = localizer.Report(false);

        DialogText = simpleReport;
        ResetSize();
        PopupCentered();
    }

    private void OnFullReport(StringName actionName)
    {
        if (actionName != "FullReport" || _gameData.CurrentPokemon is null)
            return;

        var localizer = LegalityLocalizationContext.Create(_gameData.CurrentPokemon.Legality(), GameInfo.CurrentLanguage);
        var verboseReport = localizer.Report(true);

        DialogText = verboseReport;
        ResetSize();
        PopupCentered();
    }
}
