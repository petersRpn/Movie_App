using Movies.Core.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Core.Models
{
    public class MovieResponse
    {
        public Guid MovieID { get; set; }
        public string? MovieName { get; set; }
        public string? Description { get; set; }
        public string? ReleaseDate { get; set; }
        public int? Rating { get; set; }

        public string? TicketPrice { get; set; }

        public string? Country { get; set; }
        public List<string>? Genre { get; set; }
        public string? Photo { get; set; }

        //It compares the current object to another object of CountryResponse type and returns true, if both values are same; otherwise returns false
        public override bool Equals(object? obj)
        {
            if (obj == null)
            {
                return false;
            }

            if (obj.GetType() != typeof(MovieResponse))
            {
                return false;
            }
            MovieResponse country_to_compare = (MovieResponse)obj;

            return MovieID == country_to_compare.MovieID && MovieName == country_to_compare.MovieName;
        }

        //returns an unique key for the current object
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }

    public static class CountryExtensions
    {
        //Converts from Country object to CountryResponse object
        public static MovieResponse ToMovieResponse(this Movie movie)
        {
            return new MovieResponse() { MovieID = movie.Id, MovieName = movie.Name, Description = movie.Description, ReleaseDate = movie.ReleaseDate, Rating= movie.Rating, TicketPrice = movie.TicketPrice, Country= movie.Country, Genre = movie.Genre, Photo = movie.Photo };
        }
    }
}
