using Microsoft.EntityFrameworkCore;
using Sakila.Core.Movies.Interfaces;
using Sakila.Core.Movies.Models;

namespace Sakila.Infrastructure.DataAccess
{
    public class InventoryRepository : IInventoryRepository
    {
        private readonly MySqlContext _mySqlContext;
        
        public InventoryRepository(MySqlContext mySqlContext)
        {
            _mySqlContext = mySqlContext;
        }

        public async Task<int> AddInventoryAsync(Inventory inventory)
        {
            await _mySqlContext.Inventory.AddAsync(inventory);
            return await _mySqlContext.SaveChangesAsync();
        }
        public async Task<IEnumerable<Inventory>> GetAllInventoryAsync()
        {
            return await _mySqlContext.Inventory.ToListAsync();
        }
        public async Task<IEnumerable<Inventory>> GetAllInventoryByFilmIdAsync(int filmId)
        {
            return await _mySqlContext.Inventory.Where(i => i.FilmId == filmId).ToListAsync();
        }
        public async Task<IEnumerable<Inventory>> GetAllInventoryByStoreIdAsync(int storeId)
        {
            return await _mySqlContext.Inventory.Where(i => i.StoreId == storeId).ToListAsync();
        }
        public async Task<Inventory> GetInventoryByInventoryIdAsync(int inventoryId)
        {
            return await _mySqlContext.Inventory.FindAsync(inventoryId);
        }
        public async Task<int> UpdateInventoryAsync(Inventory inventory)
        {
            _mySqlContext.Inventory.Update(inventory);
            return await _mySqlContext.SaveChangesAsync();
        }
        public async Task<int> DeleteInventoryAsync(Inventory inventory)
        {
            _mySqlContext.Inventory.Remove(inventory);

            return await _mySqlContext.SaveChangesAsync();
        }
    }
}
