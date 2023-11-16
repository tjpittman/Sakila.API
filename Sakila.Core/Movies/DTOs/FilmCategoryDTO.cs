namespace Sakila.Core.Movies.DTOs
{
    public class FilmCategoryDto
    {
        public int FilmId { get; set; }
        public int CategoryId { get; set; }
        public DateTime FilmCategoryLastUpdate { get; set; }

        public IEnumerable<CategoryDTO> CategoryDtos { get; set; }
    }
}
