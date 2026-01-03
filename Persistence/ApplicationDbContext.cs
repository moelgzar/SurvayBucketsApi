using System.Data.Common;
using System.Reflection;
using System.Security.Claims;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using SurvayBucketsApi.Entites;
using SurvayBucketsApi.Extensions;
using SurvayBucketsApi.Persistence.EntitesCnfigrations;

namespace SurvayBucketsApi.Persistence;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options , IHttpContextAccessor httpContextAccessor) :
    IdentityDbContext<ApplicationUser>(options)
{
    private readonly IHttpContextAccessor _httpContextAccessor = httpContextAccessor;

    public  DbSet<Poll> polls {  get; set; }
    public  DbSet<Answer> Answers{  get; set; }
    public  DbSet<Question> Questions {  get; set; }
    public DbSet<Vote> Votes { get; set; }
    public DbSet<VoteAnswer> VoteAnswers { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

        var cascadeFKs = modelBuilder.Model.GetEntityTypes()
            .SelectMany(fk=> fk.GetForeignKeys())
            .Where(fk=>!fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

        foreach (var fk in cascadeFKs)
            fk.DeleteBehavior = DeleteBehavior.Restrict;



        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {

        var entiries = ChangeTracker.Entries<AuditableEntity>();

        foreach (var entry in entiries)
        {
            var CurrentUserID = _httpContextAccessor.HttpContext?.User.GetUserId();


            if (entry.State == EntityState.Added)
            {
                entry.Property(x => x.CreatedByID).CurrentValue = CurrentUserID;
            }
            else if(entry.State == EntityState.Modified)

            {

                entry.Property(x => x.UpdatedByID).CurrentValue = CurrentUserID;
                entry.Property(x=>x.UpdatedOn).CurrentValue = DateTime.Now;

            }
        }


        return base.SaveChangesAsync(cancellationToken);
    }
}
