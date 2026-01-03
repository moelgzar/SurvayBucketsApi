
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SurvayBucketsApi.Authorization;
using SurvayBucketsApi.Entites;
using SurvayBucketsApi.Errors;
using SurvayBucketsApi.Persistence;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDependancy(builder.Configuration);

var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();
app.UseAuthorization();
//app.MapIdentityApi<ApplicationUser>();
app.MapControllers();
app.UseExceptionHandler();
app.Run();
