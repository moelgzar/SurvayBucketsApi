using System.Reflection;
using System.Text;
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.IdentityModel.Tokens;
using SurvayBucketsApi.Authorization;
using SurvayBucketsApi.Entites;
using SurvayBucketsApi.Errors;
using SurvayBucketsApi.Settings;

namespace SurvayBucketsApi;

public static class DependancyInjection
{
    public static IServiceCollection AddDependancy(this IServiceCollection services, IConfiguration configuration)
    {


        services.AddControllers();

        //add connection

        var connection = configuration.GetConnectionString("defaultConnection") ??
            throw new InvalidOperationException("the defult connection not found ");

        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connection));
        //--------------------------------

        // Add Policy.

        //var AllowOrign = configuration.GetSection("AllowOrigns").Get<string[]>();

        services.AddCors(option =>


        option.AddDefaultPolicy(

            buillder => buillder
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowAnyOrigin()

                ));


        // Add Hybrid Cache services
        services.AddHybridCache();






        services.
            AddMappingServices().
            AddFluentValidationServices().
            AddSwaggerServices().
            AddAuthonticationService(configuration)
           .AddBackgroundJobsServices(configuration);


        services.AddScoped<IPollservice, Pollservice>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IQuestionServices, QuestionServices>();
        services.AddScoped<IVoteServices, VoteServices>();
        services.AddScoped<IResultService, ResultService>();
        services.AddScoped<INotificationService , NotificationService>();
        //services.AddScoped<EmailService>();
        services.AddScoped<IEmailSender, EmailService>();
        //services.AddScoped<ICashService, CashService>();




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

    public static IServiceCollection AddAuthonticationService(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<ApplicationDbContext>()
            .AddDefaultTokenProviders();
            

        services.AddHttpContextAccessor();

        //services.AddIdentityApiEndpoints<ApplicationUser>()
        //  .AddEntityFrameworkStores<ApplicationDbContext>();


        services.AddSingleton<IJwtProvider, JwtProvider>();

        services.AddExceptionHandler<GlobalExceptionHandeling>();
        services.AddProblemDetails();

        //services.Configure<JwtOptions>(configuration.GetSection(JwtOptions.SectionName));

        services.AddOptions<JwtOptions>()
            .BindConfiguration(JwtOptions.SectionName)
            .ValidateDataAnnotations();
        // .validationOnstatrt --> if i wanna buld fail cuz user see excption in production suddenly. so i can use that.




        services.Configure<MailSettings>(configuration.GetSection(nameof(MailSettings)));


        var JwtSetting = configuration.GetSection(JwtOptions.SectionName).Get<JwtOptions>();


        // Configure Authentication with JWT Bearer
        services.AddAuthentication(options =>
        {
            // Set JWT Bearer as the default authentication scheme
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;

            // Set JWT Bearer as the default challenge scheme (when authentication fails)
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(o =>
        {
            // Save the token in the authentication properties
            o.SaveToken = true;

            // Configure token validation parameters
            o.TokenValidationParameters = new TokenValidationParameters
            {
                // Validate the signing key to ensure token hasn't been tampered with
                ValidateIssuerSigningKey = true,

                // Validate the issuer (who created the token)
                ValidateIssuer = true,

                // Validate the audience (who the token is intended for)
                ValidateAudience = true,

                // Validate the token's expiration time
                ValidateLifetime = true,

                // The symmetric security key used to validate the token signature
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSetting.Key)),

                // The valid issuer that we expect in the token
                ValidIssuer = JwtSetting.Issuer,

                // The valid audience that we expect in the token
                ValidAudience = JwtSetting.Audiance,

            };

        });

        services.Configure<IdentityOptions>(options =>
        {
            options.Password.RequiredLength = 8;
            options.SignIn.RequireConfirmedEmail = true;
            options.User.RequireUniqueEmail = true;

        });

        return services;
    }
    public static IServiceCollection AddBackgroundJobsServices(this IServiceCollection services , IConfiguration configuration)
    {

        services.AddHangfire(config => config
       .SetDataCompatibilityLevel(CompatibilityLevel.Version_180)
       .UseSimpleAssemblyNameTypeSerializer()
       .UseRecommendedSerializerSettings()
       .UseSqlServerStorage(configuration.GetConnectionString("HangfireConnection")));

        // Add the processing server as IHostedService
        services.AddHangfireServer();

        return services;
    }

}


