using System.Reflection;
using Microsoft.EntityFrameworkCore;
using SurvayBucketsApi.Persistence;

namespace SurvayBucketsApi;

public static class DependancyInjection
{
  public static  IServiceCollection AddDependancy(this IServiceCollection services , IConfiguration configuration)
    {


       services.AddControllers();

        //add connection

        var connection = configuration.GetConnectionString("defaultConnection") ??
            throw new InvalidOperationException("the defult connection not found ");

        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connection));
       //--------------------------------



        services.
            AddMappingServices().
            AddFluentValidationServices().
            AddSwaggerServices();
        

        services.AddScoped<IPollservice, Pollservice>();

        return services;
    }

    public static IServiceCollection AddSwaggerServices(this IServiceCollection services)
    {

        // Add services to the container.

        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        services.AddEndpointsApiExplorer();
        services.AddSwaggerGen();
      

        return services;
    }
    public static IServiceCollection AddFluentValidationServices(this IServiceCollection services)
    {


        //builder.Services.AddScoped<IValidator<CrearePollRequest>, CreatePollRequestValidator>();
        services.
             AddFluentValidationAutoValidation().
             AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        //Add Mapster configrations globally.

      

        return services;
    }

    public static IServiceCollection AddMappingServices(this IServiceCollection services)
    {

        //Add Mapster configrations globally.

        var mapcongig = TypeAdapterConfig.GlobalSettings;
        mapcongig.Scan(Assembly.GetExecutingAssembly());

        services.AddSingleton<IMapper>(new Mapper(mapcongig));


        return services;
    }
}
