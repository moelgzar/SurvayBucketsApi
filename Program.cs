
using Hangfire;
using HangfireBasicAuthenticationFilter;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;


var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDependancy(builder.Configuration);

builder.Host.UseSerilog(( (context , config) => config
    .ReadFrom.Configuration(context.Configuration)
    ));


var app = builder.Build();



// Configure the HTTP request pipeline.

    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();

        app.UseSwaggerUI( options => 
        {
            var descriptions = app.DescribeApiVersions();
               foreach (var description in descriptions)
               {
                   options.SwaggerEndpoint(
                       $"/swagger/{description.GroupName}/swagger.json",
                       description.GroupName.ToUpperInvariant()
                   );
            }
        });
    }


app.UseSerilogRequestLogging();

app.UseHttpsRedirection();
app.UseHangfireDashboard("/jobs" , new DashboardOptions

{
    Authorization = [

        new HangfireCustomBasicAuthenticationFilter{

            User = app.Configuration.GetValue<string>("HangFireSettings:username") ,
            Pass = app.Configuration.GetValue<string>("HangFireSettings:password")
        }

        ] 
}
    
    
    );


var scopefactory = app.Services.GetRequiredService<IServiceScopeFactory>();

using var scope = scopefactory.CreateScope();

var notificationservice = scope.ServiceProvider.GetRequiredService<INotificationService>();

RecurringJob.AddOrUpdate("SendNewPollsNotifications", () => notificationservice.SendNewPollsNotifications(null) , Cron.Daily);

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors();
app.UseAuthorization();
//app.UseOutputCache();
//app.MapIdentityApi<ApplicationUser>();
app.MapControllers();
app.UseExceptionHandler();
app.UseRateLimiter();
app.MapHealthChecks("health" , new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    
});
app.Run();
