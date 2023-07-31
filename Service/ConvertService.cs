using Packup.Model;
using Packup.Model.Database;

namespace Packup.Service;

public class ConvertService : ILoteParameters
{
    public static readonly ConvertService Instance = new ConvertService();

    public ConvertService() {
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

    public int GetPalletBoxes(IPalletBody pbody) =>
        pbody.Levels * BoxesByLevel + pbody.Boxes;

    private int getLoteBoxes(IPalletBody pbody) =>
        pbody.Number * BoxesByPallet + GetPalletBoxes(pbody);

    public int GetLoteBoxes(PalletBody pbody) =>
        getLoteBoxes(pbody.Absolute());

    public int GetTagNumber(IPalletBody pbody) =>
        61 - GetPalletBoxes(pbody);

    public bool IsValidTagNumber(int tagNumber) =>
        2 >= tagNumber && tagNumber <= 61;

    private TagsEntity createTagsEntity(LoteEntity lote, IPalletBody pbody) =>
        new TagsEntity(lote.Id, pbody.Number, GetTagNumber(pbody));

    public TagsEntity CreateTagsEntity(LoteEntity lote, PalletBody pbody) =>
        createTagsEntity(lote, pbody.Relative());

    public TagsEntity CreateTagsEntity(LoteEntity lote, int palletNumber) =>
        new TagsEntity(lote.Id, palletNumber);
}
