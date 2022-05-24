using AutoFixture;
using CleanArchitecture.Domain;
using CleanArchitecture.Infrastructure.Persistence;
using CleanArchitecture.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Moq;
using System;
using System.Linq;


namespace CleanArchitecture.Application.UnitTests.Mocks
{
    public static class MockVideoRepository
    {
        public static Mock<VideoRepository> GetVideoRepository()
        {
            var fixture = new Fixture();
            
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var videos = fixture.CreateMany<Video>().ToList();
            
            //Adding fake data
            videos.Add(fixture.Build<Video>()
                .With(tr => tr.CreatedBy, "system").Create());

            var options = new DbContextOptionsBuilder<StreamerDbContext>()
                .UseInMemoryDatabase(databaseName: $"StreamerDbContext-{Guid.NewGuid()}")
                .Options;

            var streamerDbContextFake = new StreamerDbContext(options);
            streamerDbContextFake.Videos!.AddRange(videos);
            streamerDbContextFake.SaveChanges();

            var mockRepository = new Mock<VideoRepository>(streamerDbContextFake);
            //Removing this line since we're using InMemory EntityFramework
            //mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(videos);
            
            return mockRepository;
        }
    }
}
