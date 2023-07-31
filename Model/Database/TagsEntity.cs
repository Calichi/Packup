using SQLite;

namespace Packup.Model.Database;

[Table("tags")]
public class TagsEntity : LoteMemberEntity
{
    public int PalletNumber { get; set; }

    public int Major { get; set; }

    public int Minor { get; set; }

    public TagsEntity(long loteId, int palletNumber, int major, int minor = 2)
    {
        LoteId = loteId;
        PalletNumber = palletNumber;
        Major = major;
        Minor = minor;
    }

    public TagsEntity(long loteId, int palletNumber) :
                 this(loteId, palletNumber, 61) { }

    public TagsEntity() { }
}
