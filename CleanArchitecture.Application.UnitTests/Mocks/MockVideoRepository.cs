﻿using AutoFixture;
using CleanArchitecture.Application.Contracts.Persistence;
using CleanArchitecture.Domain;
using Moq;
using System.Linq;


namespace CleanArchitecture.Application.UnitTests.Mocks
{
    public static class MockVideoRepository
    {
        public static Mock<IVideoRepository> GetVideoRepository()
        {
            var fixture = new Fixture();
            var videos = fixture.CreateMany<Video>().ToList();

            var mockRepository = new Mock<IVideoRepository>();
            mockRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(videos);
            
            return mockRepository;
        }
    }
}
