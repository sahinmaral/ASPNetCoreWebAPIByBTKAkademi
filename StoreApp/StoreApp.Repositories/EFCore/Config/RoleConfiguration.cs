using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace StoreApp.Repositories.Config;

public class RoleConfiguration : IEntityTypeConfiguration<IdentityRole>
{
    public void Configure(EntityTypeBuilder<IdentityRole> builder)
    {
        builder.ToTable(name: "Roles");

        builder.HasData(
            new IdentityRole() { Name = "User", NormalizedName = "USER", ConcurrencyStamp = Guid.NewGuid().ToString() },
            new IdentityRole() { Name = "Editor", NormalizedName = "EDITOR", ConcurrencyStamp = Guid.NewGuid().ToString() },
            new IdentityRole() { Name = "Admin", NormalizedName = "ADMIN", ConcurrencyStamp = Guid.NewGuid().ToString() }
        );
    }
}
