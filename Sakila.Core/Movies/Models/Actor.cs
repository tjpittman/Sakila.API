using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sakila.Core.Movies.Models
{
    public class Actor
    {
        [Column("actor_id")] public int ActorId { get; set; }
        [Column("first_name")] public string FirstName { get; set; }
        [Column("last_name")] public string LastName { get; set; }
        [Column("last_update")] public DateTime LastUpdate { get; set; }
    }
}
