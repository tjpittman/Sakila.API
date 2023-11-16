using Microsoft.EntityFrameworkCore;
using Sakila.Core.Movies.Interfaces;
using Sakila.Core.Movies.Models;

namespace Sakila.Infrastructure.DataAccess
{
    public class FilmTextRepository : IFilmTextRepository
    {
        private readonly MySqlContext _mySqlContext;
        
        public FilmTextRepository(MySqlContext mySqlContext)
        {
            _mySqlContext = mySqlContext;
        }

        public async Task<int> AddFilmText(FilmText filmText)
        {
            await _mySqlContext.FilmText.AddAsync(filmText);

            return await _mySqlContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<FilmText>> GetAllFilmText()
        {
            return await _mySqlContext.FilmText.ToListAsync();
        }

        public async Task<FilmText> GetFilmTextByfilmId(int filmId)
        {
            return await _mySqlContext.FilmText.FindAsync(filmId);
        }

        public async Task<int> UpdateFilmText(FilmText filmText)
        {
            _mySqlContext.FilmText.Update(filmText);

            return await _mySqlContext.SaveChangesAsync();
        }

        public async Task<int> DeleteFilmTextByFilmId(int filmId)
        {
            var filmText = await GetFilmTextByfilmId(filmId);

            _mySqlContext.FilmText.Remove(filmText);

            return await _mySqlContext.SaveChangesAsync();
        }
    }
}
