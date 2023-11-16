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
        public async Task<int> AddInventory(Inventory inventory)
        {
            await _mySqlContext.Inventory.AddAsync(inventory);
            return await _mySqlContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<Inventory>> GetAllInventory()
        {
            return await _mySqlContext.Inventory.ToListAsync();
        }

        public async Task<Inventory> GetInventoryByInventoryId(int inventoryId)
        {
            return await _mySqlContext.Inventory.FindAsync(inventoryId);
        }

        public async Task<int> UpdateInventory(Inventory inventory)
        {
            _mySqlContext.Inventory.Update(inventory);
            return await _mySqlContext.SaveChangesAsync();
        }

        public async Task<int> RemoveInventoryByInventoryId(int inventoryId)
        {
            var inventory = await GetInventoryByInventoryId(inventoryId);

            _mySqlContext.Inventory.Remove(inventory);

            return await _mySqlContext.SaveChangesAsync();
        }
    }
}
