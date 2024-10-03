using RallyObedienceApp.Persistency.Models;
using SQLite;

namespace RallyObedienceApp.Persistency;

public class DbService<T> where T : class, IItem, new()
{
    protected SQLiteAsyncConnection Database = null!;

    protected async virtual Task Init()
    {
        if (Database is not null)
            return;

        Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);

        try 
        { 
            var result = await Database.CreateTableAsync<T>();
        } catch (SQLiteException e)
        {
            Console.WriteLine(e.Message);
        }
    }

    public async Task<List<T>> GetItemsAsync()
    {
        await Init();

        return await Database.Table<T>().ToListAsync();
    }

    public async Task<List<T>> GetItemsNotDoneAsync()
    {
        await Init();

        return await Database.Table<T>().Where(t => t.Done).ToListAsync();

        // SQL queries are also possible
        //return await Database.QueryAsync<TodoItem>("SELECT * FROM [TodoItem] WHERE [Done] = 0");
    }

    public async Task<T> GetItemAsync(string id)
    {
        await Init();

        return await Database.Table<T>().Where(i => i.ID == id).FirstOrDefaultAsync();
    }

    public async Task<int> SaveItemAsync(T item)
    {
        await Init();

        if (item.ID != string.Empty)
            return await Database.UpdateAsync(item);
        else
            return await Database.InsertAsync(item);
    }

    public async Task<int> DeleteItemAsync(T item)
    {
        await Init();

        return await Database.DeleteAsync(item);
    }
}

