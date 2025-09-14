using SQLite;

namespace ContactSyncApp.Dal;

public class ContactSyncDatabase<T> where T : new()
{
    private const string DatabaseFilename = "ContactSync.db3";

    private const SQLiteOpenFlags Flags =
        SQLiteOpenFlags.ReadWrite |
        SQLiteOpenFlags.Create |
        SQLiteOpenFlags.SharedCache;

    private string DatabasePath =>
        Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);

    private readonly SQLiteAsyncConnection _database;

    public ContactSyncDatabase()
    {
        if (_database != null)
            return;

        _database = new SQLiteAsyncConnection(DatabasePath, Flags);
        _database.CreateTableAsync<T>().Wait();
    }

    public async Task<List<T>> GetAllAsync() => await _database.Table<T>().ToListAsync();
    public async Task<T> GetByIdAsync(int id) => await _database.FindAsync<T>(id);
    public async Task<int> AddAsync(T record) => await _database.InsertAsync(record);
    public async Task<int> UpdateAsync(T record) => await _database.UpdateAsync(record);
    public async Task<int> DeleteAsync(T record) => await _database.DeleteAsync(record);
}

