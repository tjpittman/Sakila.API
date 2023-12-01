using Sakila.Core.Movies.Models;

namespace Sakila.Core.Movies.Interfaces
{
    public interface IInventoryRepository
    {
        public Task<int> AddInventoryAsync(Inventory inventory);
        public Task<IEnumerable<Inventory>> GetAllInventoryAsync();
        public Task<IEnumerable<Inventory>> GetAllInventoryByFilmIdAsync(int filmId);
        public Task<IEnumerable<Inventory>> GetAllInventoryByStoreIdAsync(int storeId);
        public Task<Inventory> GetInventoryByInventoryIdAsync(int inventoryId);
        public Task<int> UpdateInventoryAsync(Inventory inventory);
        public Task<int> DeleteInventoryAsync(Inventory inventory);
    }
}
