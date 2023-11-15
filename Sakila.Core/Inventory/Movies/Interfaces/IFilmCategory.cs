using Sakila.Core.Inventory.Movies.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakila.Core.Inventory.Movies.Interfaces
{
    public interface IFilmCategory
    {
        public Task<IEnumerable<FilmCategory>> GetFilmCategoriesAsync();
        public Task<FilmCategory?> GetFilmCategoryByFilmIdCategoryIdAsync(int filmId, int categoryId);
        public Task<int> AddFilmCategoryAsync(FilmCategory filmCategory);
        public Task<int> UpdateFilmCategoryAsync(FilmCategory filmCategory);
        public Task<int> DeleteFilmCategoryByFilmIdCategoryIdAsync(int filmId, int categoryId);
        public Task<int> DeleteFilmCategoryAsync(FilmCategory filmCategory);
    }
}
