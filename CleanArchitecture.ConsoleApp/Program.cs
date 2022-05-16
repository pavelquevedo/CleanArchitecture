


using CleanArchitecture.Data;
using CleanArchitecture.Domain;
using Microsoft.EntityFrameworkCore;

StreamerDbContext streamerDbContext = new();

await QueryMethods();

async Task QueryWithLinq()
{
    var streamers = await (from i in streamerDbContext.Streamers
                           where EF.Functions.Like(i.Name, "%a%")
                           select i).ToListAsync();

    foreach (var streamer in streamers)
    {
        Console.WriteLine($"{streamer.Id} - {streamer.Name}");
    }
}

async Task QueryMethods()
{
    var streamers = streamerDbContext.Streamers;
    //FirstAsync will assume that the entry exists, but if not it will throw an exception
    var firstAsync = await streamers.Where(x => x.Name.Contains("a")).FirstAsync();
    //FirstOrDefault will return a null value if the entry doesn't exists
    var firstOrDefaultAsync = await streamers.Where(x => x.Name.Contains("a")).FirstOrDefaultAsync();

    var firstOrDefaultAsync_v2 = await streamers.FirstOrDefaultAsync(x => x.Name.Contains("a"));
    
    //Using entity framework functions, in this case "LIKE"
    var firstOrDefaultAsync_v3 = await streamers.FirstOrDefaultAsync(x => EF.Functions.Like(x.Name, "%a%"));

    //If the resultset is more than 1 record, or is null it will throw an exception
    var singleAsync = await streamers.Where(x => x.Id == 1).SingleAsync();
    //If the resultset is empty it will return a null value
    var singleOrDefaultAsync = await streamers.Where(x => x.Id == 1).SingleOrDefaultAsync();

    //It will search by the primary key
    var findAsync = await streamers.FindAsync(1);
}


