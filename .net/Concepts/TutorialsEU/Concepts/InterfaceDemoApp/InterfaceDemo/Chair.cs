using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceDemo
{
    internal class Chair : Furniture, IDestroyable
    {
        public Chair(string color, string material)
        {
            this.Color = color;
            this.Material = material;
            DestructionSound = "chairDes";
        }

        public string DestructionSound { get; set; }

        public void Destroy()
        {
            Console.WriteLine($"The {Color} is destroyed");
            Console.WriteLine($"Playing destruction sound {DestructionSound}");
            Console.WriteLine("Spawning chair parts");
        }
    }
}
