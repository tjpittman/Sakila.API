using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sakila.Core.Inventory.Movies.Models
{
    public class Category
    {
        [Key,Column("category_id")] 
        public int CategoryId { get; set; }

        [Column("name")] 
        public string Name { get; set; }

        [Column("last_update")] 
        public DateTime LastUpdate { get; set; }
    }
}
