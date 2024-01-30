using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Core.Models
{
    public class GetMovieByIdResponse
    {
        public Guid MovieID { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public string? ReleaseDate { get; set; }
        public int? Rating { get; set; }

        public string? TicketPrice { get; set; }

        public string? Country { get; set; }
        public List<string>? Genre { get; set; }
        public string? Photo { get; set; }

    }
}
