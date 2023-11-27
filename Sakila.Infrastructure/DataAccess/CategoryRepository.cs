using Microsoft.EntityFrameworkCore;
using Sakila.Core.Movies.Interfaces;
using Sakila.Core.Movies.Models;

namespace Sakila.Infrastructure.DataAccess
{
    public class CategoryRepository : ICategoryRepository
    {

        private readonly MySqlContext _mySqlContext;

        public CategoryRepository(MySqlContext mySqlContext)
        {
            _mySqlContext = mySqlContext;
        }

        public async Task<int> AddNewCategoryAsync(Category category)
        {
            _mySqlContext.Category.Add(category);

            return await _mySqlContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Category>> GetAllCategoriesAsync()
        {
            return await _mySqlContext.Category.ToListAsync();
        }
        public async Task<Category> GetCategoryByCategoryIdAsync(int categoryId)
        {
            return await _mySqlContext.Category.FindAsync(categoryId);
        }
        
        public async Task<int> UpdateCategoryAsync(Category category)
        {
            _mySqlContext.Category.Update(category);

            return await _mySqlContext.SaveChangesAsync();
        }
        
        public async Task<int> DeleteCategoryAsync(Category category)
        {
            _mySqlContext.Category.Remove(category);

            return await _mySqlContext.SaveChangesAsync();
        }
    }
}
