using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurvayBucketsApi.Abstractions.Const;
using SurvayBucketsApi.Entites;

namespace SurvayBucketsApi.Persistence.EntitesCnfigrations;

public class UserConfigration : IEntityTypeConfiguration<ApplicationUser>
{
    public void Configure(EntityTypeBuilder<ApplicationUser> builder)
    {

        builder.
            OwnsMany(x => x.RefreshTokens)
            .ToTable("RefreshTokens")
            .WithOwner()
            .HasForeignKey("UserId");

        builder.Property(x => x.FirstName).HasMaxLength(100);
        builder.Property(x => x.LastName).HasMaxLength(100);

        // add defaualt user (seeding )

        var passhasher = new PasswordHasher<ApplicationUser>();

        builder.HasData(new ApplicationUser
        {
            Id = DefaultUser.AdminId,
            Email = DefaultUser.AdminEmail,
            EmailConfirmed = true,

            PasswordHash = DefaultUser.AdminPasswordHash,
            SecurityStamp = DefaultUser.SecurityStamp,
            ConcurrencyStamp = DefaultUser.ConcurrencyStamp,
            FirstName = "Survey Basket",
            LastName = "Admin",
            NormalizedEmail = DefaultUser.AdminEmail.ToUpper(),
            NormalizedUserName = DefaultUser.AdminEmail.ToUpper(),
            UserName = DefaultUser.AdminEmail,

        }

            );

    }
}
