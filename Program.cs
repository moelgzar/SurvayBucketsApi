
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Serilog;
using SurvayBucketsApi.Authorization;
using SurvayBucketsApi.Entites;
using SurvayBucketsApi.Errors;
using SurvayBucketsApi.Persistence;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDependancy(builder.Configuration);

//builder.Services.AddMemoryCache();
builder.Services.AddDistributedMemoryCache();

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

app.UseCors();
app.UseAuthorization();
//app.UseOutputCache();
//app.MapIdentityApi<ApplicationUser>();
app.MapControllers();
app.UseExceptionHandler();
app.Run();
