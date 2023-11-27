using Microsoft.EntityFrameworkCore;
using Sakila.Core.Movies.Interfaces;
using Sakila.Core.Movies.Models;

namespace Sakila.Infrastructure.DataAccess
{
    public class FilmCategoryRepository : IFilmCategoryRepository
    {
        private readonly MySqlContext _mySqlContext;

        public FilmCategoryRepository(MySqlContext mySqlContext)
        {
            _mySqlContext = mySqlContext;
        }

        public async Task<int> AddFilmCategoryAsync(FilmCategory filmCategory)
        {
            _mySqlContext.FilmCategory.Add(filmCategory);

            return await _mySqlContext.SaveChangesAsync();
        }
        
        public async Task<IEnumerable<FilmCategory>> GetFilmCategoriesAsync()
        {
            return await _mySqlContext.FilmCategory.ToListAsync();
        }

        public async Task<List<FilmCategory>> GetFilmCategoriesByFilmIdAsync(int filmId)
        {
            return await _mySqlContext.FilmCategory.Where(fc => fc.FilmId == filmId).ToListAsync();
        }

        public async Task<FilmCategory?> GetFilmCategoryByFilmIdCategoryIdAsync(int filmId, int categoryId)
        {
            return await _mySqlContext.FilmCategory.FindAsync(filmId, categoryId);
        }

        public async Task<int> UpdateFilmCategoryAsync(FilmCategory filmCategory)
        {
            _mySqlContext.FilmCategory.Update(filmCategory);

            return await _mySqlContext.SaveChangesAsync();
        }
        
        public async Task<int> DeleteFilmCategoryAsync(FilmCategory filmCategory)
        {
            _mySqlContext.FilmCategory.Remove(filmCategory);

            return await _mySqlContext.SaveChangesAsync();
        }
 
    }
}
