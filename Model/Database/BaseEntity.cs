using SQLite;

namespace Packup.Model.Database;

public class BaseEntity
{
    [PrimaryKey, AutoIncrement]
    public long Id { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.Now;
}
