using RallyObedienceApp.Persistency.Data.Exercises;
using RallyObedienceApp.Persistency.Models;
using SQLite;

namespace RallyObedienceApp.Persistency;

public class ExerciseDbService
{
    private const string DatabaseFilename = "ExerciseDb.db3";
    private string DatabasePath => Path.Combine(FileSystem.AppDataDirectory, DatabaseFilename);
    SQLiteAsyncConnection? Database;

    public async Task InitAsync()
    {
        if (Database is not null)
            return;

        Database = new SQLiteAsyncConnection(DatabasePath);
        var result = await Database.CreateTableAsync<ExerciseItem>();
        await Database.CreateTableAsync<ExercisePartial>();

        await Database.InsertOrReplaceAsync(Z_001.CreateMain());

        foreach (var partial in Z_001.CreatePartials())
            await Database.InsertOrReplaceAsync(partial);
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
