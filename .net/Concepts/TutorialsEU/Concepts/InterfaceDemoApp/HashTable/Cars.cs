using System;
using System.Collections.Generic;
using System.Text;

namespace HashTable
{
    class Car
    {
        public string Make { get; set; }
        public string Model { get; set; }
    }

    class InitializeCars
    {
        public static void Main()
        {
            var cars = new List<Car>()
            {
                new Car(){Make="Proton", Model="Iriz"},
            };

        }
    }
}
