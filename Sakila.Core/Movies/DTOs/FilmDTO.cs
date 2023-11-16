namespace Sakila.Core.Movies.DTOs
{
    public class FilmDTO
    {

        public int FilmId { get; set; }

        public string Title { get; set; }

        public string? Description { get; set; }

        public int? ReleaseYear { get; set; }

        public int LanguageId { get; set; }

        public int? OriginalLanguageId { get; set; }

        public int RentalDuration { get; set; }

        public decimal RentalRate { get; set; }

        public int? Length { get; set; }

        public decimal ReplacementCost { get; set; }

        public string? Rating { get; set; }
        public string? SpecialFeatures { get; set; }
        public DateTime FilmLastUpdate { get; set; }

        //Should I really nest away the different DTO containers...
        //seems clunky for a DTO when I can just dump everything into a MovieDTO and have omit the junk that may not be needed?
        //  i.e category.category_id or language.language_id
        public IEnumerable<FilmCategoryDto> FilmCategoryDTOs { get; set; }
    }
}
