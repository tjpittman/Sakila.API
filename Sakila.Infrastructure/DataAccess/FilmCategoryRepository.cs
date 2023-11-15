using Microsoft.EntityFrameworkCore;

using Sakila.Core.Inventory.Movies.Interfaces;
using Sakila.Core.Inventory.Movies.Models;

namespace Sakila.Infrastructure.DataAccess
{
    public class FilmCategoryRepository : IFilmCategory
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
        public async Task<FilmCategory?> GetFilmCategoryByFilmIdCategoryIdAsync(int filmId, int categoryId)
        {
            return await _mySqlContext.FilmCategory.Where(fc => 
                                                          fc.FilmId == filmId && 
                                                          fc.CategoryId == categoryId).FirstOrDefaultAsync();
        }

        public async Task<int> UpdateFilmCategoryAsync(FilmCategory filmCategory)
        {
            _mySqlContext.FilmCategory.Update(filmCategory);

            return await _mySqlContext.SaveChangesAsync();
        }
        
        public async Task<int> DeleteFilmCategoryByFilmIdCategoryIdAsync(int filmId, int categoryId)
        {
            var filmCategory = await GetFilmCategoryByFilmIdCategoryIdAsync(filmId, categoryId);

            if (filmCategory == null)
                return 0;

            return await DeleteFilmCategoryAsync(filmCategory);
        }
        public async Task<int> DeleteFilmCategoryAsync(FilmCategory filmCategory)
        {
            _mySqlContext.FilmCategory.Remove(filmCategory);

            return await _mySqlContext.SaveChangesAsync();
        }
    }
}
