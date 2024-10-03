using RallyObedienceApp.Persistency.Data.Parkours.RO_Z;
using RallyObedienceApp.Persistency.Models;
using SQLite;

namespace RallyObedienceApp.Persistency;

public class ParkourDbService
{
    private const string DatabaseFilename = "ParkourDb.db3";
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
                await Database.DropTableAsync<ParkourItem>();
            }
            catch (SQLiteException)
            {
                ; // ignore it
            }
            var result = await Database.CreateTableAsync<ParkourItem>();

            try
            {
                await Database.DropTableAsync<PositionDto>();
            }
            catch (SQLiteException)
            {
                ; // ignore it
            }
            await Database.CreateTableAsync<PositionDto>();

            try
            {
                await Database.DropTableAsync<PositionExercises>();
            }
            catch (SQLiteException)
            {
                ; // ignore it
            }
            await Database.CreateTableAsync<PositionExercises>();

            await AddToDb(Parkour_00001.Create());
        }
        finally
        {
            semaphoreSlim.Release();
        }
    }

    private async Task AddToDb(ParkourItem parkour)
    {
        await Database!.InsertAsync(parkour);

        foreach (var position in parkour.Positions)
        {
            await Database.InsertAsync(position);
            foreach (var exercise in position.Exercises)
            {
                await Database.InsertAsync(exercise);
            }
        }
    }

    public async Task<ParkourItem?> GetItemAsync(string id)
    {
        await InitAsync();

        ParkourItem? parkourItem;

        parkourItem = await Database!
                                .Table<ParkourItem>()
                                .Where(i => i.ID == id)
                                .FirstOrDefaultAsync();

        if (parkourItem is not null)
            parkourItem.Positions = await Database!
                                            .Table<PositionDto>()
                                            .Where(p => p.ParkourID == id)
                                            .ToListAsync();

        if (parkourItem is not null && parkourItem.Positions is not null && parkourItem.Positions.Any())
            foreach (var position in parkourItem.Positions)
            {
                position.Exercises = await Database!
                                                .Table<PositionExercises>()
                                                .Where(e => e.PositionID == position.ID)
                                                .ToListAsync();
            }

        return parkourItem;
    }
}
