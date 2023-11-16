using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sakila.Core.Movies.Models
{
    public class FilmText
    {
        [Key, Column("film_id")]
        public int FilmId { get; set; }
        [Column("title")]
        public string Title { get; set; }
        [Column("description")]
        public string Description { get; set; }
    }
}
