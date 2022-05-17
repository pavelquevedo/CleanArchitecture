using CleanArchitecture.Data;
using CleanArchitecture.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitecture.ConsoleApp.Operations
{
    public class Insert
    {
        StreamerDbContext _dbContext;

        public Insert(StreamerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddNewStreamerWithVideo()
        {
            var pantaya = new Streamer
            {
                Name = "Pantaya",
                Url = "https://www.pantaya.com"
            };

            var hungerGames = new Video
            {
                Name = "Hunger Games",
                Streamer = pantaya
            };

            await _dbContext.Videos.AddAsync(hungerGames);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddNewVideoWithStreamerId()
        {
            var video = new Video
            {
                Name = "Batman Forever",
                StreamerId = 3
            };

            await _dbContext.Videos.AddAsync(video);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddNewActorWithVideo()
        {
            var actor = new Actor
            {
                FirstName = "Brad",
                LastName = "Pitt"
            };

            await _dbContext.AddAsync(actor);
            await _dbContext.SaveChangesAsync();
            
            var videoActor = new VideoActor()
            {
                ActorId = actor.Id,
                VideoId = 4
            };

            await _dbContext.AddAsync(videoActor);
            await _dbContext.SaveChangesAsync();
        }

        public async Task AddNewDirectorWithVideo()
        {
            var director = new Director()
            {
                FirstName = "David",
                LastName = "Fincher",
                VideoId = 4
            };

            await _dbContext.Directors.AddAsync(director);
            await _dbContext.SaveChangesAsync();
        }
    }
}
