using SQLite;
using Packup.Model.Entity;

namespace Packup.Service;

public class RepositoryService
{
    public static readonly RepositoryService Instance = new RepositoryService();

    private static SQLiteAsyncConnection database;

    private static async Task Init()
    {
        if (database is not null) return;

        string dbPath = Path.Combine(FileSystem.AppDataDirectory, "database.db3");
        database = new SQLiteAsyncConnection(dbPath);

        await database.CreateTableAsync<Lote>();
        await database.CreateTableAsync<Tags>();
        await database.CreateTableAsync<State>();
    }

    private RepositoryService() {
    }

    public async Task SaveAsync<T>(T data) where T : Base {
        await Init();
        await database.InsertAsync(data);
    }

    public async Task<List<Lote>> GetOperations() {
        await Init();
        return await database.Table<Lote>().ToListAsync();
    }

    public async Task<List<T>> GetMembers<T>(Lote lote) where T : LoteMember, new() {
        await Init();
        List<T> members = await database.Table<T>().ToListAsync();
        return (from member in members
                where member.LoteId == lote.Id
                select member).ToList();
    }

    public void GetMembers<T>(Lote lote, Action<Task<List<T>>> receiver) where T : LoteMember, new() {
        GetMembers<T>(lote).ContinueWith(receiver);
    }
}
