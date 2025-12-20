
using Microsoft.EntityFrameworkCore;
using SurvayBucketsApi.Entites;
using SurvayBucketsApi.Persistence;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddDependancy(builder.Configuration);


//builder.Services.AddIdentityApiEndpoints<ApplicationUser>()
//    .AddEntityFrameworkStores<ApplicationDbContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

//app.MapIdentityApi<ApplicationUser>();
app.MapControllers();

app.Run();
