


using CleanArchitecture.Data;
using CleanArchitecture.Domain;
using Microsoft.EntityFrameworkCore;

StreamerDbContext streamerDbContext = new();

await QueryMethods();

async Task TrackingAndNotTracking()
{
    //By default a query from entity framework is tracked, this means this objects can be updated 
    var streamerWithTracking = await streamerDbContext.Streamers.FirstOrDefaultAsync(x => x.Id == 1);

    //If we ask EF to not track, we won't be able to update these objects. This is recommended for larger queries
    var streamerWithNoTracking = await streamerDbContext.Streamers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == 2);

    streamerWithTracking.Name = "Netflix Plus";
    streamerWithNoTracking.Name = "Amazon Plus";

    await streamerDbContext.SaveChangesAsync();
}

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
    var firstOrDefaultAsync = await streamers.Where(x => x.Name.Contains("ama"))
        .Include(v => v.Videos).FirstOrDefaultAsync();

    var firstOrDefaultAsync_v2 = await streamers.FirstOrDefaultAsync(x => x.Name.Contains("a"));
    
    //Using entity framework functions, in this case "LIKE"
    var firstOrDefaultAsync_v3 = await streamers.FirstOrDefaultAsync(x => EF.Functions.Like(x.Name, "%a%"));

    //If the resultset is more than 1 record, or is null it will throw an exception
    var singleAsync = await streamers.Where(x => x.Id == 1).SingleAsync();
    //If the resultset is empty it will return a null value
    var singleOrDefaultAsync = await streamers.Where(x => x.Id == 1).SingleOrDefaultAsync();

    //It will search by the primary key
    var findAsync = await streamers.FindAsync(2);

    foreach (var item in firstOrDefaultAsync.Videos)
    {
        Console.WriteLine($"{item.Id} - {item.Name}");
    }
}


