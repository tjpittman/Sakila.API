using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakila.Core.Inventory.Movies.DTOs
{
    public class FilmCategoryDto
    {
        public int FilmId { get; set; }
        public int CategoryId { get; set; }
        public DateTime FilmCategoryLastUpdate { get; set; }

        public IEnumerable<CategoryDTO> CategoryDtos{ get; set; }
    }
}
