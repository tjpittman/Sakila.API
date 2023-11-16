using Sakila.Core.Movies.Models;

namespace Sakila.Core.Movies.Interfaces
{
    public interface IFilmTextRepository
    {
        public Task<int> AddFilmText(FilmText filmText);
        public Task<IEnumerable<FilmText>> GetAllFilmText();
        public Task<FilmText> GetFilmTextByfilmId(int filmId);
        public Task<int> UpdateFilmText(FilmText filmText);
        public Task<int> DeleteFilmTextByFilmId(int filmId);
    }
}
