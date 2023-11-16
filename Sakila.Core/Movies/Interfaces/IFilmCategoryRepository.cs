using Sakila.Core.Movies.Models;

namespace Sakila.Core.Movies.Interfaces
{
    public interface IFilmCategoryRepository
    {
        public Task<IEnumerable<FilmCategory>> GetFilmCategoriesAsync();
        public Task<FilmCategory?> GetFilmCategoryByFilmIdCategoryIdAsync(int filmId, int categoryId);
        public Task<int> AddFilmCategoryAsync(FilmCategory filmCategory);
        public Task<int> UpdateFilmCategoryAsync(FilmCategory filmCategory);
        public Task<int> DeleteFilmCategoryAsync(FilmCategory filmCategory);//TODO: change to go by ID field.
    }
}
