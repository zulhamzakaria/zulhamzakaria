using MinimalAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MinimalAPI.Repositories
{
    public class UserRepository
    {
        public static List<User> Users = new()
        {
            new() { Username = "admin", EmailAddress = "admin@email.com", Password = "adminPassword!", Givenname = "Admin", Surname = "Admin", Role = "Administrator" },
            new() { Username = "janitor", EmailAddress = "janitor@email.com", Password = "janitorPassword!", Givenname = "Janitor", Surname = "Janitor", Role = "Janitor" },
            new() { Username = "hr", EmailAddress = "hr@email.com", Password = "hrPassword!", Givenname = "HR", Surname = "HR", Role = "HumanResource" },
        };
    }
}
