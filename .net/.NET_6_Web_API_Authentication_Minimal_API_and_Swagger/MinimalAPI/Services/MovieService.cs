using MinimalAPI.Models;
using MinimalAPI.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinimalAPI.Services
{
    public class MovieService : IMovieService
    {
        public Movie CreateMovie(Movie movie)
        {
            movie.Id = MovieRepository.movies.Count + 1;
            MovieRepository.movies.Add(movie);
            return movie;
        }

        public bool Delete(int id)
        {
            var result = MovieRepository.movies.FirstOrDefault(o => o.Id == id);
            if (result is null)
                return false;
            MovieRepository.movies.Remove(result);
            return true;

        }

        public Movie Get(int id)
        {
            var result = MovieRepository.movies.FirstOrDefault(o => o.Id == id);
            if (result is null) return null;
            return result;
        }

        public List<Movie> List()
        {
            var result = MovieRepository.movies;
            return result;
        }

        public Movie Update(Movie movie)
        {
            var oldMovie = MovieRepository.movies.FirstOrDefault(o => o.Id == movie.Id);
            if (oldMovie is null)
                return null;
            oldMovie.Title = movie.Title;
            oldMovie.Description = movie.Description;
            oldMovie.Rating = movie.Rating;

            return movie;

        }
    }
}
