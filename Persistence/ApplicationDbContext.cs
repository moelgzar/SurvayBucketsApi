using System.Data.Common;
using System.Reflection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SurvayBucketsApi.Entites;
using SurvayBucketsApi.Persistence.EntitesCnfigrations;

namespace SurvayBucketsApi.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
    IdentityDbContext<ApplicationUser>(options)
{
   public  DbSet<Poll> polls {  get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}
