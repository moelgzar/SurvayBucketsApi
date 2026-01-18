using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurvayBucketsApi.Abstractions.Const;
using SurvayBucketsApi.Entites;

namespace SurvayBucketsApi.Persistence.EntitesCnfigrations;

public class UserRoleConfigration : IEntityTypeConfiguration<IdentityUserRole<string>>
{
    public void Configure(EntityTypeBuilder<IdentityUserRole<string>> builder)
    {

      
        // add defaualt userRole (seeding )


        builder.HasData(new IdentityUserRole<string>
        {
            RoleId = DefaultRole.AdminRoleId , 
            UserId = DefaultUser.AdminId ,

        }

            );
       
    }
}
