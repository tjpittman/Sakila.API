using Microsoft.EntityFrameworkCore;
using Sakila.Core.Movies.Interfaces;
using Sakila.Core.Movies.Models;

namespace Sakila.Infrastructure.DataAccess
{
    public class FilmRepository : IFilmRepository
    {
        private readonly MySqlContext _mySqlContext;

        public FilmRepository(MySqlContext mySqlContext)
        {
            _mySqlContext = mySqlContext;
        }

        public async Task<IEnumerable<Film>> GetAllFilmsAsync()
        {
            return await _mySqlContext.Film.ToListAsync();
        }
        public async Task<Film?> GetFilmByFilmIdAsync(int filmId)
        {
            return await _mySqlContext.Film.FindAsync(filmId);
        }

        public async Task<int> AddFilmAsync(Film film)
        {
            _mySqlContext.Film.Add(film);

            return await _mySqlContext.SaveChangesAsync();
        }

        public async Task<int> UpdateFilmAsync(Film film)
        {
            _mySqlContext.Film.Update(film);

            return await _mySqlContext.SaveChangesAsync();
        }
        
        public async Task<int> DeleteFilmAsync(Film film)
        {
            _mySqlContext.Film.Remove(film);

            return await _mySqlContext.SaveChangesAsync();
        }
    }
}
