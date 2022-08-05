using CloudCustomers.API.Models;
using System.Collections.Generic;

namespace CloudCustomers.UnitTests.Fixtures
{
    static class UsersFixture
    {
        public static List<User> GetTestUsers() => new()
            {
                new User
                {
                    Name = "TestUser1",
                    Email = "tu1@mail.com",
                    Address = new Address
                    {
                        AddressLine1 = "Market St",
                        City = "Market",
                        Zipcode = "266902"
                    }
                },
                new User
                {
                    Name = "TestUSer2",
                    Email = "tu2@mail.com",
                    Address= new Address
                    {
                        AddressLine1 = "Lot 84, Kg Tengah",
                        City="Segamat",
                        Zipcode ="85000"
                    }
                }
            };
    }
}
