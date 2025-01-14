﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace MGTConcerts.Models
{
    public enum Genre
    {
        Rock,
        Pop,
        Metal,
        Rap,
        Indie,
        Electro
    }

    public class Concert
    {
        public int Id { get; set; }

        [Required]
        public string? ConcertName { get; set; }

        public int ArtistId { get; set; }
        [ForeignKey("ArtistId")]
        [ValidateNever]
        [JsonIgnore]
        public Artist? Artist { get; set; }
        public string? ArtistName
        {
            get
            {
                if (Artist != null) return Artist.Name;
                return "";
            }
        }

        public string? ButtonColor { get; set;}
        
        [Required]
        public DateTime? ConcertDate { get; set; }

        public string? ConcertDateStr
        {
            get
            {
                if (ConcertDate != null)
                {


                    return ConcertDate.Value.Day.ToString() + "/" +
                        ConcertDate.Value.Month.ToString() + "/" +
                        ConcertDate.Value.Year.ToString();
                       
                        
                }
                return null;
            }
        }

        [Required]
        [Range(1, 1000)]
        public double Price { get; set; }

        public int MusicVenueId { get; set; }
        [ForeignKey("MusicVenueId")]
        [ValidateNever]
        public MusicVenue? MusicVenue { get; set; }

        public Genre? Genre { get; set; }

        [ValidateNever]
        public string? ImageUrl { get; set; }

        //temporary variable for the reccomendation fuzzy score whatever
        [NotMapped]
        public double FuzzyScore{ get; set; }
    }
}
