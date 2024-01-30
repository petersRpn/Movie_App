using Azure;
using Movies.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Movies.Infrastructure.Interfaces
{
     public interface IMovieApi
    {
        Task<List<MovieResponse>> GetMovie();
        Task<MovieResponse> GetMovieById(GetMovieByIdRequest id);

        Task<MovieResponse> PostMovie(PostMovieRequest movie);

        Task<MovieResponse> UpdateMovie(UpdateMovieRequest movie);

        bool DeleteMovie(Guid id);
        

    }
    
}
