﻿using Sakila.Core.Movies.Models;

namespace Sakila.Core.Movies.Interfaces
{
    public interface IFilmCategoryRepository
    {
        public Task<IEnumerable<FilmCategory>> GetFilmCategoriesAsync();
        public Task<List<FilmCategory>> GetFilmCategoriesByFilmIdAsync(int filmId);
        public Task<List<FilmCategory>> GetFilmCategoriesByCategoryIdAsync(int categoryId);
        public Task<FilmCategory?> GetFilmCategoryByFilmIdCategoryIdAsync(int filmId, int categoryId);
        public Task<int> AddFilmCategoryAsync(FilmCategory filmCategory);
        public Task<int> UpdateFilmCategoryAsync(FilmCategory filmCategory);
        public Task<int> DeleteFilmCategoryAsync(FilmCategory filmCategory);
    }
}
