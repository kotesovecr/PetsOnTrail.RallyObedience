using RallyObedienceApp.Persistency.Data.Exercises.RO_Z;
using RallyObedienceApp.Persistency.Models;
using SQLite;

namespace RallyObedienceApp.Persistency;

public class ExerciseDbService
{
    private const string DatabaseFilename = "ExerciseDb.db3";
    private string DatabasePath => Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);
    SQLiteAsyncConnection? Database;
    static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(1, 1);

    public async Task InitAsync()
    {
        await semaphoreSlim.WaitAsync();
        try
        {
            if (Database is not null)
            {
                return;
            }

            Database = new SQLiteAsyncConnection(DatabasePath);

            try
            {
                await Database.DropTableAsync<ExerciseItem>();
            }
            catch (SQLiteException)
            {
                ; // ignore it
            }
            var result = await Database.CreateTableAsync<ExerciseItem>();

            try
            {
                await Database.DropTableAsync<ExercisePartial>();
            }
            catch (SQLiteException)
            {
                ; // ignore it
            }
            await Database.CreateTableAsync<ExercisePartial>();

            await AddToDb(Start.CreateMain(), Start.CreatePartials());
            await AddToDb(Finish.CreateMain(), Finish.CreatePartials());
            await AddToDb(D0a.CreateMain(), D0a.CreatePartials());
            await AddToDb(Z_001.CreateMain(), Z_001.CreatePartials());
        }
        finally
        {
            semaphoreSlim.Release();
        }
    }

    private async Task AddToDb(ExerciseItem exerciseItem, List<ExercisePartial> partials)
    {
        await Database!.InsertAsync(exerciseItem);

        foreach (var partial in partials)
            await Database.InsertAsync(partial);
    }

    public async Task InsertAsync(ExerciseItem exerciseItem)
    {
        await InitAsync();

        await Database!.InsertOrReplaceAsync(exerciseItem);
    }

    public async Task<ExerciseItem?> GetItemAsync(string id)
    {
        await InitAsync();

        ExerciseItem? exerciseItem;

        exerciseItem = await Database!
                                .Table<ExerciseItem>()
                                .Where(i => i.ID == id)
                                .FirstOrDefaultAsync();

        if (exerciseItem is not null)
            exerciseItem.Partials = await Database!
                                            .Table<ExercisePartial>()
                                            .Where(p => p.ExerciseID == id)
                                            .ToListAsync();

        return exerciseItem;
    }
}
