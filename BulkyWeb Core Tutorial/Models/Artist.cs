using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace MGTConcerts.Models
{
    public class Artist
    {
        public int Id { get; set; }

        [Required]
        public string? Name { get; set; }

        public string? Biography { get; set; }

        public List<Concert>? Concerts { get; set; }

        public string? Period
        {
            get
            {

                if(Concerts != null)
                {

                    if (Concerts.Count > 1)
                    {
                        if (Concerts[0].ConcertDate != null && Concerts.Last().ConcertDate != null)
                        {
                            return Concerts[0].ConcertDate.Value.Day.ToString() + "/" +
                                Concerts[0].ConcertDate.Value.Month.ToString() +
                                " - " + Concerts.LastOrDefault().ConcertDate.Value.Day.ToString() + "/" +
                                Concerts.LastOrDefault().ConcertDate.Value.Month.ToString();
                        }
                    }
                    else if(Concerts.Count == 1)
                    {
                        return Concerts[0].ConcertDate.Value.Day.ToString() + "/" +
                                Concerts[0].ConcertDate.Value.Month.ToString() + "/" +
                                   Concerts[0].ConcertDate.Value.Year.ToString();
;
                    }
                }
                return null;
            }
        }

        [ValidateNever]
        public string? ImageUrl { get; set; }
    }
}