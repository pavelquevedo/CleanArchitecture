using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Identity.Configurations
{
    public class UserRoleConfiguration : IEntityTypeConfiguration<IdentityUserRole<string>>
    {
        public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
        {
            builder.HasData(
                new IdentityUserRole<string>
                {
                    RoleId = "eeb77705-527b-49bb-828f-fa2b1756c5cb",
                    UserId = "deaa9cd1-6686-42bd-a53f-bd49c4670d23"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "b9be869c-cb7d-4a49-a6ab-1f1704decc09",
                    UserId = "4554301a-ce83-4b4e-a30c-a4d9c1a59ffc"
                }
            );
        }
    }
}
