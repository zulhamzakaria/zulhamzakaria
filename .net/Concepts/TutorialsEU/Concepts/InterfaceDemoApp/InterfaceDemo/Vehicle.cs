using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceDemo
{
    internal class Vehicle
    {
        public float Speed { get; set; }
        public string Color { get; set; }
        public Vehicle()
        {
            Speed = 120f;
            Color = "White";
        }
        public Vehicle(float speed, string color)
        {
            Speed = speed;
            Color = color;
        }
    }
}
