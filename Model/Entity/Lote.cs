using SQLite;
using System.Globalization;

namespace Packup.Model.Entity;

[Table("lotes")]
public class Lote : Base, ILoteParameters
{
    public int ReceivedBoxes { get; set; }

    public int PalletsByLote { get; set; }

    public int LevelsByPallet { get; set; }

    public int BoxesByLevel { get; set; }

    public Lote(ILoteParameters lParams, int receivedBoxes) {
        this.ReceivedBoxes = receivedBoxes;
        this.PalletsByLote = lParams.PalletsByLote;
        this.LevelsByPallet = lParams.LevelsByPallet;
        this.BoxesByLevel = lParams.BoxesByLevel;
    }

    public Lote() { }
}
