using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurvayBucketsApi.Abstractions.Const;

namespace SurvayBucketsApi.Persistence.EntitesCnfigrations;

public class RoleClaimsConfigration : IEntityTypeConfiguration<IdentityRoleClaim<string>>
{
    public void Configure(EntityTypeBuilder<IdentityRoleClaim<string>> builder)
    {

        var permissions = Permissions.GetAllPermissions();

        var Adminclaims = new List <IdentityRoleClaim<string>>();

        for (int i = 0; i < permissions.Count; i++)
        {


            Adminclaims.Add(new IdentityRoleClaim<string>
            {

                Id = i + 1,
                ClaimType = Permissions.Type,
                ClaimValue = permissions[i],
                RoleId = DefaultRole.AdminRoleId

            }
            );
        }

        builder.HasData(Adminclaims);
       
    }
}
