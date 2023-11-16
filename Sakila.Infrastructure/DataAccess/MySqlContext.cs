using Microsoft.EntityFrameworkCore;
using Sakila.Core.Movies.Models;

namespace Sakila.Infrastructure.DataAccess
{
    public class MySqlContext : DbContext
    {
        public DbSet<Film> Film { get; set; }
        public DbSet<FilmActor> FilmActor { get; set; }
        public DbSet<FilmCategory> FilmCategory { get; set; }
        public DbSet<FilmText> FilmText { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Language> Language { get; set; }
        public DbSet<Actor> Actor { get; set; }
        public DbSet<Inventory> Inventory { get; set; } 
 
        public MySqlContext(DbContextOptions options) : base(options) { }
    }
}
