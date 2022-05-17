


using CleanArchitecture.ConsoleApp.Operations;
using CleanArchitecture.Data;
using CleanArchitecture.Domain;
using Microsoft.EntityFrameworkCore;

StreamerDbContext streamerDbContext = new();

Insert insertOperations = new Insert(streamerDbContext);

Query queryOperations = new Query(streamerDbContext);

await queryOperations.QueryToMultipleEntities();




