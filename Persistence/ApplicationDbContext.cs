using System.Reflection;
using SurvayBucketsApi.Persistence.EntitesCnfigrations;

namespace SurvayBucketsApi.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
   public  DbSet<Poll> polls {  get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}
