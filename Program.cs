using Microsoft.EntityFrameworkCore;
using RoyalVilla_API.Controllers;
using RoyalVilla_API.Database;
using RoyalVilla_API.dtos;
using RoyalVilla_API.Models;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

// Add services to the container.
builder.Services.AddDbContext<ApplicationDbContext>(opt => opt.UseSqlServer(connectionString));
//builder.Services.AddAutoMapper(opt =>
//{
//  //<Source,Destination>
//    opt.CreateMap<VillaCreateDTO, Villa>();
//    opt.CreateMap<UpdateVillaDTO, Villa>();
//    opt.CreateMap<Villa, VillaDTO>();
//});

builder.Services.AddAutoMapper(opt=> { },typeof(Program));

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();
await SeedDataAsync(app);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

static async Task SeedDataAsync(WebApplication app)
{
    using var scope=app.Services.CreateScope();
    var context=scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

    await context.Database.MigrateAsync();
    
}


