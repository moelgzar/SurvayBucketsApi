using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SurvayBucketsApi.Abstractions.Const;
using SurvayBucketsApi.Entites;

namespace SurvayBucketsApi.Persistence.EntitesCnfigrations;

public class RoleConfigration : IEntityTypeConfiguration<ApplicationRole>
{
    public void Configure(EntityTypeBuilder<ApplicationRole> builder)
    {


        // add defaualt user (seeding )


        builder.HasData([

            new ApplicationRole {
                Id = DefaultRole.AdminRoleId ,
                Name = DefaultRole.Admin ,
                NormalizedName = DefaultRole.Admin.ToUpper() ,
                ConcurrencyStamp = DefaultRole.AdminConcurrencyStamp

            } ,

            new ApplicationRole {
                Id = DefaultRole.MemberRoleId ,
                Name = DefaultRole.Member ,
                NormalizedName = DefaultRole.Member.ToUpper() ,
                ConcurrencyStamp = DefaultRole.MemberConcurrencyStamp,
                IsDefault = true
            }
            ]);




    }
}
