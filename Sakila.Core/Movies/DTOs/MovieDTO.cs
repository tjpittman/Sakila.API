using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Sakila.Core.Movies.DTOs
{
    public class MovieDTO
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public int? ReleaseYear { get; set; }
        public string Language { get; set; }
        public string? OriginalLanguage { get; set; }
        public int RentalDuration { get; set; }
        public decimal RentalRate { get; set; }
        public int? Length { get; set; }
        public string? Rating { get; set; }
        public string? SpecialFeatures { get; set; }
        public IEnumerable<CategoryDTO>? Categories { get; set; }
        public IEnumerable<ActorDTO>? Actors { get; set; }
    }
}
