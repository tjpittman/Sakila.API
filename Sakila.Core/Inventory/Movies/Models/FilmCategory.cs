using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace Sakila.Core.Inventory.Movies.Models
{
    public class FilmCategory
    {
        [Key, Column("film_id", Order = 0)]
        public int FilmId { get; set; }
        [Key, Column("category_id", Order=1)]
        public int CategoryId { get; set; }
        [Column("last_update")] 
        public DateTime LastUpdate{ get; set; }
    }
}
