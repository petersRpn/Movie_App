using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Movies.Core.Entities;
using Movies.Core.Models;
using Movies.Core.Repositories.Interfaces;
using Movies.Infrastructure.Interfaces;
using System.Diagnostics;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net.NetworkInformation;
using System.Diagnostics.Metrics;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;

namespace Movies.Core.Services
{
    public class MovieApi : IMovieApi
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;

        public MovieApi(IUnitOfWork unitOfWork, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _config = configuration;
        }

        public Task<List<MovieResponse>> GetMovie()
        {
            try
            {
               
                var movieRepository = _unitOfWork.GetRepository<Movie>();
                var getResult = movieRepository.GetAll();
               
                return Task.FromResult(getResult.Select(x => x.ToMovieResponse()).ToList());

            }
            catch (Exception ex)
            {
                throw;

            }
            
           

        }

        public Task<MovieResponse> GetMovieById(GetMovieByIdRequest movie)
        {
            if (movie.Id == null)
                return null;

            var movieRepository = _unitOfWork.GetRepository<Movie>(); //getResult
            var  movie_response_from_list = movieRepository.FirstOrDefault(temp => temp.Id == movie.Id);
            

            if (movie_response_from_list == null)
                return null;

            return Task.FromResult(movie_response_from_list.ToMovieResponse());
        }

        public Task<MovieResponse> PostMovie(PostMovieRequest movie)
        {

            var validate = Validator(movie);

          
            
            
            //Convert object from MovieAddRequest to Movie type
            Movie movies = movie.ToMovie();
            if (validate.isError == false)
            {
                //generate MovieID
                movies.Id = Guid.NewGuid();

                //Add movie object into _Movies
                var movieRepository = _unitOfWork.GetRepository<Movie>();
                movieRepository.Add(movies);
                _unitOfWork.SaveChanges();
            }
            

            return Task.FromResult(movies.ToMovieResponse());
        }

        public Task<MovieResponse> UpdateMovie(UpdateMovieRequest movie)
        {
            var movieRepository = _unitOfWork.GetRepository<Movie>();
            var UpdateMovie = movieRepository.FirstOrDefault(x => x.Name == movie.Name);

            if(UpdateMovie == null)
            {
                throw new Exception("Movie not found");
            }
            UpdateMovie.TicketPrice = movie.TicketPrice;
            UpdateMovie.Country = movie.Country;
            UpdateMovie.Rating = (int)movie.Rating;
            UpdateMovie.Genre = movie.Genre;
            UpdateMovie.Name = movie.Name;
            UpdateMovie.Description = movie.Description;
            UpdateMovie.Photo = movie.Photo;
            UpdateMovie.ReleaseDate = movie.ReleaseDate;

            movieRepository.Update(UpdateMovie);
            _unitOfWork.SaveChanges();

            var UpdatedMovie = movieRepository.FirstOrDefault(x => x.Name == movie.Name);

            return Task.FromResult(UpdatedMovie.ToMovieResponse());


        }

        public bool DeleteMovie(Guid id)
        {
            var movieRepository = _unitOfWork.GetRepository<Movie>();
            var deleteMovie = movieRepository.FirstOrDefault(x => x.Id == id);
            if (deleteMovie == null)
            {
                return false;
            }
            movieRepository.Remove(deleteMovie);
            _unitOfWork.SaveChanges();
            return true;

        }
        private (bool isError, string messge) Validator(PostMovieRequest movie)
        {
            //Validation: AddRequest parameter can't be null
            if (movie == null)
            {
                throw new ArgumentNullException(nameof(movie));
            }

            //Validation:  Name can't be null
            if (movie.Name == null)
            {
                throw new ArgumentException(nameof(movie.Name));
            }

            //Validation: Name can't be duplicate
            var movieRepository = _unitOfWork.GetRepository<Movie>();
            if (movieRepository.Any(temp => temp.Name == movie.Name))
            {
                throw new ArgumentException("Given country name already exists");
            }
            if (string.IsNullOrEmpty(movie.Description))
            {
                throw new ArgumentException("Description can not be empty");
            }
            if(string.IsNullOrEmpty(movie.Country))
            {
                throw new ArgumentException("Country can not be empty");
            }
            if(movie.Genre == null || movie.Genre.Count < 1)
            {
                throw new ArgumentException("Genre can not be empty");
            }
            if(string.IsNullOrEmpty(movie.TicketPrice))
            {
                throw new ArgumentException("TicketPrice can not be empty");
            }
            if(string.IsNullOrEmpty(movie.Photo))
            {
                throw new ArgumentException("Photo can not be empty");
            }

            return (false, "success");
        }

        
    }
}
