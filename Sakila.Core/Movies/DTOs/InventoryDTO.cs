namespace Sakila.Core.Movies.DTOs
{
    public class InventoryDTO
    {
        public int InventoryId { get; set; } 
        public int FilmId { get; set; } 
        public int StoreId { get; set; } 
        public DateTime LastUpdate { get; set; }
    }
}
