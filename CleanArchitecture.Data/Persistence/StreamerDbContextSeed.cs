using CleanArchitecture.Domain;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.Infrastructure.Persistence
{
    public class StreamerDbContextSeed
    {
        public static async Task SeedAsync(StreamerDbContext context, ILogger<StreamerDbContextSeed> logger)
        {
            if (!context.Streamers.Any())
            {
                context.Streamers.AddRange(GetPreconfiguredStreamer());
                await context.SaveChangesAsync();
                logger.LogInformation("We're inserting new records to the database {context}", typeof(StreamerDbContext).Name);
            }
        }

        private static IEnumerable<Streamer> GetPreconfiguredStreamer()
        {
            return new List<Streamer>()
            {
                new Streamer{CreatedBy = "Pavel Quevedo", Name = "Maxi HBP", Url="http://www.hbp.com"}
                new Streamer{CreatedBy = "Pavel Quevedo", Name = "Amazon VIP", Url="http://www.amazonvip.com"}
            };
        }
    }
}
