using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

using Microsoft.EntityFrameworkCore;

namespace Sakila.Core.Movies.Models
{
    [Table("film_category")]
    public class FilmCategory
    {
        [Column("film_id")]
        public int FilmId { get; set; }
        [Column("category_id")]
        public int CategoryId { get; set; }
        [Column("last_update")]
        public DateTime LastUpdate { get; set; }
    }
}
