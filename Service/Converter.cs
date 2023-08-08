using Packup.Model;

namespace Packup.Service;

public class Converter : ILoteParameters
{
    public static readonly Converter Instance = new Converter();
    public Converter() {
        SetParameters(LoteParameters.Default);
    }

    public void SetParameters(LoteParameters lParams) {
        PalletsByLote = lParams.PalletsByLote;
        LevelsByPallet = lParams.LevelsByPallet;
        BoxesByLevel = lParams.BoxesByLevel;
        BoxesByPallet = lParams.BoxesByPallet;
        BoxesByLote = lParams.BoxesByLote;
    }

    public int PalletsByLote { get; private set; }

    public int LevelsByPallet { get; private set; }

    public int BoxesByLevel { get; private set; }

    public int BoxesByPallet { get; private set;}

    public int BoxesByLote { get; private set; }

    public int GetPalletProducedBoxes(IPalletBody pbody) =>
        pbody.Levels * BoxesByLevel + pbody.Boxes;

    public int GetPalletPendingBoxes(IPalletBody pbody) =>
        BoxesByPallet - GetPalletProducedBoxes(pbody);

    public int GetPalletPendingBoxes(int tagNumber) =>
        tagNumber - 2;

    public int GetPalletProducedBoxes(int tagNumber) =>
        BoxesByPallet - GetPalletPendingBoxes(tagNumber);

    public int GetPreviousTagNumberValid(int tagNumber) {
        int previous = tagNumber + 1;
        return previous > 61 ? 2 : previous;
    }

    public int GetProducedBoxes(int previousTagNumber, int currentTagNumber) {
        if (currentTagNumber > previousTagNumber)
            return GetPalletProducedBoxes(currentTagNumber) + GetPalletPendingBoxes(previousTagNumber);

        return previousTagNumber - currentTagNumber;
    }

    private int getLoteBoxes(IPalletBody pbody) =>
        pbody.Number * BoxesByPallet + GetPalletProducedBoxes(pbody);

    public int GetLoteBoxes(PalletBody pbody) =>
        getLoteBoxes(pbody.Absolute());

    public int GetTagNumber(IPalletBody pbody) =>
        61 - GetPalletProducedBoxes(pbody);

    public bool IsValidTagNumber(int tagNumber) =>
        2 >= tagNumber && tagNumber <= 61;

    private Model.Entity.Tags createTagsEntity(Model.Entity.Lote lote, IPalletBody pbody) =>
        new Model.Entity.Tags(lote.Id, pbody.Number, GetTagNumber(pbody));

    public Model.Entity.Tags CreateTagsEntity(Model.Entity.Lote lote, PalletBody pbody) =>
        createTagsEntity(lote, pbody.Relative());

    public Model.Entity.Tags CreateTagsEntity(Model.Entity.Lote lote, int palletNumber) =>
        new Model.Entity.Tags(lote.Id, palletNumber);

    public PalletBody CreatePalletBody(int boxes) {
        int number = Math.DivRem(boxes, BoxesByPallet, out boxes);
        int levels = Math.DivRem(boxes, BoxesByLevel, out boxes);
        return new PalletBody(number, levels, boxes, true);
    }

    public PalletBody CreatePalletBody(int palletNumber, int tagNumber) {
        int boxes;
        int levels = Math.DivRem(GetPalletProducedBoxes(tagNumber), BoxesByLevel, out boxes);
        return new PalletBody(palletNumber, levels, boxes, false);
    }
}
