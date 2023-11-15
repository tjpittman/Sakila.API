using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Sakila.Core.Inventory.Movies.Models;

namespace Sakila.Core.Inventory.Movies.Interfaces
{
    public interface IFilmRepository
    {

        public Task<IEnumerable<Film>> GetAllFilmsAsync();
        public Task<Film?> GetFilmByFilmIdAsync(int filmId);
        public Task<int> AddFilmAsync(Film film);
        public Task<int> UpdateFilmAsync(Film film);
        public Task<int> DeleteFilmByFilmIdAsync(int filmId);
        public Task<int> DeleteFilm(Film film);

    }
}
