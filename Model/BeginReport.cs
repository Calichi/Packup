namespace Packup.Model;

public struct BeginReport
{
    public BeginReport(BeginParameters parameters, Service.Converter converter) {
        LotePendingBoxes = converter.GetLoteBoxes(parameters.LastPallet) - converter.GetLoteBoxes(parameters.FirstPallet);
        PalletProducedBoxes = converter.GetPalletProducedBoxes(parameters.FirstPallet);
        PalletPendingBoxes = converter.GetPalletPendingBoxes(parameters.FirstPallet);
        NextTag = converter.GetTagNumber(parameters.FirstPallet);
    }

    public int LotePendingBoxes { get; }
    public int PalletProducedBoxes { get; }
    public int PalletPendingBoxes { get; }
    public int NextTag { get; }
}
