﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Sakila.Core.Movies.Models
{
    public class Film
    {
        [Column("film_id")]
        public int FilmId { get; set; }

        [Column("title")]
        public string Title { get; set; }

        [Column("description")]
        public string? Description { get; set; }

        [Column("release_year")]
        public int? ReleaseYear { get; set; }

        [Column("language_id")]
        public int LanguageId { get; set; }

        [Column("original_language_id"), DefaultValue(null)]
        public int? OriginalLanguageId { get; set; }

        [Column("rental_duration")]
        public int RentalDuration { get; set; }

        [Column("rental_rate")]
        public decimal RentalRate { get; set; }

        [Column("length")]
        public int? Length { get; set; }

        [Column("replacement_cost")]
        public decimal ReplacementCost { get; set; }

        [Column("rating")]
        public string? Rating { get; set; }

        [Column("special_features")]
        public string? SpecialFeatures { get; set; }

        [Column("last_update")] 
        public DateTime LastUpdate { get; set; }
    }

}
