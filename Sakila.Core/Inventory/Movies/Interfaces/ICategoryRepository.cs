using Sakila.Core.Inventory.Movies.Models;

namespace Sakila.Core.Inventory.Movies.Interfaces
{
    public interface ICategoryRepository
    {
        public Task<IEnumerable<Category>> GetAllCategoriesAsync();
        public Task<Category?> GetCategoryByCategoryIdAsync(int categoryId);
        public Task<int> AddNewCategoryAsync(Category category);

        public Task<int> UpdateCategoryAsync(Category category);
        public Task<int> DeleteCategoryByCategoryIdAsync(int categoryId);
        public Task<int> DeleteCategoryAsync(Category category);
    }
}
