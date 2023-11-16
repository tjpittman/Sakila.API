using Sakila.Core.Movies.Models;

namespace Sakila.Core.Movies.Interfaces
{
    public interface IFilmRepository
    {

        public Task<IEnumerable<Film>> GetAllFilmsAsync();
        public Task<Film?> GetFilmByFilmIdAsync(int filmId);
        public Task<int> AddFilmAsync(Film film);
        public Task<int> UpdateFilmAsync(Film film);
        public Task<int> DeleteFilmAsync(Film film);//TODO: change to go by id field

    }
}
