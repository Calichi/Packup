
namespace Packup.Model;

public struct LoteParameters : ILoteParameters
{
    static LoteParameters()
    {
        Default = new LoteParameters(15, 10, 6);
    }

    public static readonly LoteParameters Default;

    public LoteParameters(int palletsByLote, int levelsByPallet, int boxesByLevel)
    {
        PalletsByLote = palletsByLote;
        LevelsByPallet = levelsByPallet;
        BoxesByLevel = boxesByLevel;
    }

    public int PalletsByLote { get; }

    public int LevelsByPallet { get; }

    public int BoxesByLevel { get; }

    public int BoxesByPallet => BoxesByLevel * LevelsByPallet;

    public int BoxesByLote => BoxesByPallet * PalletsByLote;
}
