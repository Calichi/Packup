using SQLite;
using Packup.Model.Database;
using System.Diagnostics;

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

        await database.CreateTableAsync<LoteEntity>();
        await database.CreateTableAsync<TagsEntity>();
    }

    private RepositoryService() {
    }

    public async Task SaveAsync(LoteEntity lote) {
        await Init();
        await database.InsertAsync(lote);
    }

    public async Task SaveAsync(TagsEntity tags) {
        await Init();
        await database.InsertAsync(tags);
    }

    public async Task<List<LoteEntity>> GetOperations() {
        await Init();
        return await database.Table<LoteEntity>().ToListAsync();
    }

    public async Task<List<TagsEntity>> GetTags(LoteEntity lote) {
        await Init();
        List<TagsEntity> allTags =  await database.Table<TagsEntity>().ToListAsync();
        return (from tags in allTags
                where tags.LoteId == lote.Id
                select tags).ToList();
    }
}
