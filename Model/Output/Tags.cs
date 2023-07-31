namespace Packup.Model.Output;

public class Tags : Database.TagsEntity
{
    public int Length { get; }
    public Tags(Database.TagsEntity data) {
        Id = data.Id;
        Timestamp = data.Timestamp;
        LoteId = data.LoteId;
        PalletNumber = data.PalletNumber;
        Major = data.Major;
        Minor = data.Minor;
        Length = Major - Minor + 1;
    }
}
