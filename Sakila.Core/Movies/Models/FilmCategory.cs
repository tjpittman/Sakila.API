using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sakila.Core.Movies.Models
{
    public class FilmCategory
    {
        [Key, Column("film_id", Order = 0)]
        public int FilmId { get; set; }
        [Key, Column("category_id", Order = 1)]
        public int CategoryId { get; set; }
        [Column("last_update")]
        public DateTime LastUpdate { get; set; }
    }
}
