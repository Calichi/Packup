using SQLite;

namespace Packup.Model.Entity;

public class Base
{
    [PrimaryKey, AutoIncrement]
    public long Id { get; set; }
    public DateTime Timestamp { get; set; } = DateTime.Now;
}
