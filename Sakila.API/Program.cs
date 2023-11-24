using Microsoft.EntityFrameworkCore;
using Sakila.Core.Movies.Interfaces;
using Sakila.Infrastructure.DataAccess;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

var connString = builder.Configuration.GetConnectionString("Default");

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IActorRepository, ActorRepository>()
    .AddTransient<ICategoryRepository, CategoryRepository>()
    .AddTransient<IFilmActorRepository, FilmActorRepository>()
    .AddTransient<IFilmCategoryRepository, FilmCategoryRepository>()
    .AddTransient<IFilmRepository, FilmRepository>()
    .AddTransient<IFilmTextRepository, FilmTextRepository>()
    .AddTransient<IInventoryRepository, InventoryRepository>()
    .AddTransient<ILanguageRepository, LanguageRepository>();

builder.Services.AddDbContext<MySqlContext>(options => options.UseMySql(connString, ServerVersion.AutoDetect(connString)));


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
