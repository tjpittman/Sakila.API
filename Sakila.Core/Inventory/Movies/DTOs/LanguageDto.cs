using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakila.Core.Inventory.Movies.DTOs
{
    public class LanguageDto
    {
        public int LanguageId { get; set; }
        public string Name { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
