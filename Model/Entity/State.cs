using SQLite;

namespace Packup.Model.Entity;

[Table("operation_states")]
public class State : LoteMember
{
    public int PalletNumber { get; set; }
    public int PreviousTagNumber { get; set; }
    public int TagNumber { get; set; }
}
