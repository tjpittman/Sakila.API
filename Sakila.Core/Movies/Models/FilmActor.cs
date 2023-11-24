using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
 

namespace Sakila.Core.Movies.Models
{
    [Table("film_actor")]
    public class FilmActor
    {
        [Column("actor_id")]
        public int ActorId { get; set; }
        [Column("film_id")]
        public int FilmId { get; set; }
        [Column("last_update")]
        public DateTime LastUpdate { get; set; }
    }
}
