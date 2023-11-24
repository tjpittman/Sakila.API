using Microsoft.EntityFrameworkCore;
using Sakila.Core.Movies.Models;

namespace Sakila.Infrastructure.DataAccess
{
    public class MySqlContext : DbContext
    {
        public DbSet<Actor> Actor { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<Film> Film { get; set; }
        public DbSet<FilmActor> FilmActor { get; set; }
        public DbSet<FilmCategory> FilmCategory { get; set; }
        public DbSet<FilmText> FilmText { get; set; }
        public DbSet<Inventory> Inventory { get; set; }
        public DbSet<Language> Language { get; set; }
 
        public MySqlContext(DbContextOptions options) : base(options) { }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Film>()
                .HasKey(f => new { f.FilmId });

            modelBuilder.Entity<Actor>()
                .HasKey(a => new { a.ActorId });

            modelBuilder.Entity<FilmActor>()
                .HasKey(fa => new { fa.FilmId, fa.ActorId });

            modelBuilder.Entity<FilmCategory>()
                .HasKey(fc => new { fc.FilmId, fc.CategoryId});

            modelBuilder.Entity<Category>()
                .HasKey(c => new { c.CategoryId});

            modelBuilder.Entity<Language>()
                .HasKey(l => new { l.LanguageId});

            modelBuilder.Entity<Inventory>()
                .HasKey(i => new { i.InventoryId });

            modelBuilder.Entity<FilmText>()
                .HasKey(ft => new { ft.FilmId });
        }
 
    }
}
