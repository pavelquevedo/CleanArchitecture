using CleanArchitecture.Data;
using CleanArchitecture.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace CleanArchitecture.ConsoleApp.Operations
{
    public class Query
    {
        private readonly StreamerDbContext _dbContext;

        public Query(StreamerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        async Task TrackingAndNotTracking()
        {
            //By default a query from entity framework is tracked, this means this objects can be updated 
            var streamerWithTracking = await _dbContext.Streamers.FirstOrDefaultAsync(x => x.Id == 1);

            //If we ask EF to not track, we won't be able to update these objects. This is recommended for larger queries
            var streamerWithNoTracking = await _dbContext.Streamers.AsNoTracking().FirstOrDefaultAsync(x => x.Id == 2);

            streamerWithTracking.Name = "Netflix Plus";
            streamerWithNoTracking.Name = "Amazon Plus";

            await _dbContext.SaveChangesAsync();
        }

        async Task QueryWithLinq()
        {
            var streamers = await (from i in _dbContext.Streamers
                                   where EF.Functions.Like(i.Name, "%a%")
                                   select i).ToListAsync();

            foreach (var streamer in streamers)
            {
                Console.WriteLine($"{streamer.Id} - {streamer.Name}");
            }
        }

        async Task QueryMethods()
        {
            var streamers = _dbContext.Streamers;
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

        public async Task QueryToMultipleEntities()
        {
            //Including actors to the videos
            //var videoWithActors = await _dbContext.Videos.Include(a => a.Actors).FirstOrDefaultAsync(q => q.Id == 4);

            ////Optimizing query and selecting a single column, or just a few columns
            //var actor = await _dbContext.Actors.Select(q => new Actor { FirstName = q.FirstName }).ToListAsync<Actor>();

            var videoWithDirector = await _dbContext.Videos
                                            .Where(d => d.Director != null)
                                            .Include(d => d.Director)
                                            .Select(q => new Video
                                            {
                                                Id = q.Id,
                                                Name = q.Name,
                                                Director = q.Director
                                            }).ToListAsync();
        }

    }
}
