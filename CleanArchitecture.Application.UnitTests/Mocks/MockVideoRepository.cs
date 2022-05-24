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
        public static void AddDataVideoRepository(StreamerDbContext streamerDbContextFake)
        {
            var fixture = new Fixture();
            
            fixture.Behaviors.Add(new OmitOnRecursionBehavior());
            var videos = fixture.CreateMany<Video>().ToList();
            
            //Adding fake data
            videos.Add(fixture.Build<Video>()
                .With(tr => tr.CreatedBy, "system").Create());

      
            streamerDbContextFake.Videos!.AddRange(videos);
            streamerDbContextFake.SaveChanges();            
        }
    }
}
