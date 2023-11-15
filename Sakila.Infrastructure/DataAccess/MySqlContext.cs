using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;
using Sakila.Core.Inventory.Movies.Models;

namespace Sakila.Infrastructure.DataAccess
{
    public class MySqlContext : DbContext
    {
        public DbSet<Film> Film { get; set; }
        public DbSet<FilmCategory> FilmCategory { get; set; }
        public DbSet<Category> Category { get; set; }

        public MySqlContext(DbContextOptions options) : base(options) { }
         

    }
}
