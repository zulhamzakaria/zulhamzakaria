using MinimalAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinimalAPI.Repositories
{
    public class MovieRepository
    {
        public static List<Movie> movies = new()
        {
            new() { Id = 1, Title = "Moonknight", Description = "About Moonknight", Rating = 10 },
            new() { Id = 1, Title = "Dune", Description = "Spice", Rating = 10 },
            new() { Id = 1, Title = "Batman", Description = "About Batman", Rating = 10 },
            new() { Id = 1, Title = "Red Notice", Description = "Thievery", Rating = 10 },
        };
    }
}
