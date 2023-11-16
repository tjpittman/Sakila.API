namespace Sakila.Core.Movies.Interfaces
{
    public interface IInventoryRepository
    {
        public Task<int> AddInventory(Models.Inventory inventory);
        public Task<IEnumerable<Models.Inventory>> GetAllInventory();
        public Task<Models.Inventory> GetInventoryByInventoryId(int inventoryId);
        public Task<int> UpdateInventory(Models.Inventory inventory);
        public Task<int> RemoveInventoryByInventoryId(int inventoryId);

    }
}
