using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sakila.Core.Inventory.Movies.Models
{
    public class Language
    {
        [Key, Column("language_id")]
        public int LanguageId { get; set; }
        [Column("name")]
        public string Name { get; set; }
        [Column("last_update")]
        public DateTime LastUpdate { get; set; }\
    }
}
