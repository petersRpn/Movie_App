using Movies.Core.Entities;
using Movies.Core.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Core.Models
{
    public class PostMovieRequest
    {
        [Key]
        public Guid MovieID { get; set; }

        [Required(ErrorMessage = "City Name can't be blank")]
        [StringLength(40, MinimumLength = 3, ErrorMessage = "{0} should be between {2} and {1} characters long")]
        [RegularExpression("^[A-Za-z .]*$", ErrorMessage = "{0} should contain only alphabets, space and dot (.)")]
        [Display(Name = "Movie Name")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "Description can't be blank")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Release Date can't be blank")]
        public string? ReleaseDate { get; set; }

        [Required(ErrorMessage = "Rating can't be blank")]
        public RatingStatus? Rating { get; set; }

        [Required(ErrorMessage = "Ticket Price can't be blank")]
        public string? TicketPrice { get; set; }

        [Required(ErrorMessage = "Country can't be blank")]
        public string? Country { get; set; }

        [Required(ErrorMessage = "Genre can't be blank")]
        public List<string>? Genre { get; set; }

        [Required(ErrorMessage = "Photo can't be blank")]
        public string? Photo { get; set; }

        public override string ToString()
        {
            return $"Person object - name: {Name}, Description: {Description}, ReleaseDate: {ReleaseDate}, Rating: {Rating}, TicketPrice: {TicketPrice}, Country: {Country}, Genre: {Genre}, Photo: {Photo}";
        }

        public Movie ToMovie()
        {
             return new Movie() { Name = Name, Description = Description, ReleaseDate = ReleaseDate, Country = Country, Genre = Genre, Photo = Photo, TicketPrice = TicketPrice, Rating = ((int?)Rating) };
            
        }
    }
}
