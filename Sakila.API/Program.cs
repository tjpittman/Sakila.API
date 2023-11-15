using Microsoft.EntityFrameworkCore;
using Sakila.Core.Inventory.Movies.Interfaces;
using Sakila.Infrastructure.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var connString = builder.Configuration.GetConnectionString("Default");


builder.Services.AddDbContext<MySqlContext>(options => options.UseMySql(connString, ServerVersion.AutoDetect(connString)));


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IFilmRepository, FilmRepository>();


var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
