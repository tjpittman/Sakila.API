using Sakila.Core.Movies.Models;

namespace Sakila.Core.Movies.Interfaces
{
    public interface ICategoryRepository
    {
        public Task<IEnumerable<Category>> GetAllCategoriesAsync();
        public Task<Category> GetCategoryByCategoryIdAsync(int categoryId);
        public Task<int> AddNewCategoryAsync(Category category);

        public Task<int> UpdateCategoryAsync(Category category);
        public Task<int> DeleteCategoryAsync(Category category);//TODO: change to go by id field
    }
}
