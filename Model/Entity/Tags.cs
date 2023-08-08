using SQLite;

namespace Packup.Model.Entity;

[Table("tags")]
public class Tags : LoteMember
{
    public int PalletNumber { get; set; }

    public int Major { get; set; }

    public int Minor { get; set; }

    public Tags(long loteId, int palletNumber, int major, int minor = 2)
    {
        LoteId = loteId;
        PalletNumber = palletNumber;
        Major = major;
        Minor = minor;
    }

    public Tags(long loteId, int palletNumber) :
                 this(loteId, palletNumber, 61) { }

    public Tags() { }
}
