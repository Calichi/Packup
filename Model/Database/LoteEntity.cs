using SQLite;
using System.Globalization;

namespace Packup.Model.Database;

[Table("lotes")]
public class LoteEntity : BaseEntity, ILoteParameters
{
    public int ReceivedBoxes { get; set; }

    public int PalletsByLote { get; set; }

    public int LevelsByPallet { get; set; }

    public int BoxesByLevel { get; set; }

    public LoteEntity(ILoteParameters lParams, int receivedBoxes) {
        this.ReceivedBoxes = receivedBoxes;
        this.PalletsByLote = lParams.PalletsByLote;
        this.LevelsByPallet = lParams.LevelsByPallet;
        this.BoxesByLevel = lParams.BoxesByLevel;
    }

    public LoteEntity() { }
}
