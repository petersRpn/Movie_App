using Movies.Core.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Core.Models
{
    public class GetMovieByIdRequest
    {
        public Guid Id { get; set; }

        public Movie ToCountry()
        {
            return new Movie() { Id = Id  };
        }
    }
}
