using CleanArchitecture.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Identity.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            var hasher = new PasswordHasher<ApplicationUser>();
            builder.HasData(
                new ApplicationUser()
                {
                    Id = "deaa9cd1-6686-42bd-a53f-bd49c4670d23",
                    Email = "admin@pavelquevedo.com",
                    NormalizedEmail = "admin@pavelquevedo.com",
                    FirstName = "Pavel",
                    LastName = "Quevedo",
                    NormalizedUserName = "pavelquevedo",
                    PasswordHash = hasher.HashPassword(null, "pass"),
                    EmailConfirmed = true
                },
                new ApplicationUser()
                {
                    Id = "4554301a-ce83-4b4e-a30c-a4d9c1a59ffc",
                    Email = "test@pavelquevedo.com",
                    NormalizedEmail = "test@pavelquevedo.com",
                    FirstName = "Test",
                    LastName = "User",
                    NormalizedUserName = "test",
                    PasswordHash = hasher.HashPassword(null, "pass"),
                    EmailConfirmed = true
                }
            );
        }
    }
}
