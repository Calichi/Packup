namespace Packup.Model;

public class Tags : Entity.Tags
{
    public int Length { get; }
    public Tags(Entity.Tags data) {
        Id = data.Id;
        Timestamp = data.Timestamp;
        LoteId = data.LoteId;
        PalletNumber = data.PalletNumber;
        Major = data.Major;
        Minor = data.Minor;
        Length = Major - Minor + 1;
    }
}
