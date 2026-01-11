
using Hangfire;
using HangfireBasicAuthenticationFilter;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using SurvayBucketsApi.Authorization;
using SurvayBucketsApi.Entites;
using SurvayBucketsApi.Errors;
using SurvayBucketsApi.Persistence;

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
    app.UseSwaggerUI();
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


app.UseCors();
app.UseAuthorization();
//app.UseOutputCache();
//app.MapIdentityApi<ApplicationUser>();
app.MapControllers();
app.UseExceptionHandler();
app.Run();
