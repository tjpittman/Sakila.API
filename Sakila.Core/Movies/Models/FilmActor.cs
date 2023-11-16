using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sakila.Core.Movies.Models
{
    public class FilmActor
    {
        [Key, Column("factor_id", Order = 0)]
        public int ActorId { get; set; }
        [Key, Column("film_id", Order = 1)]
        public int FilmId { get; set; }
        [Column("last_update")]
        public DateTime LastUpdate { get; set; }

    }
}
