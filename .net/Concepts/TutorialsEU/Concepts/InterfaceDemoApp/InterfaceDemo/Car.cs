using System;
using System.Collections.Generic;
using System.Text;

namespace InterfaceDemo
{
    internal class Car : Vehicle, IDestroyable
    {
        public Car(float speed, string color)
        {
            this.Speed = speed;
            this.Color = color;
            DestructionSound = "";
            DestroyablesNearby = new List<IDestroyable>();
        }

        public string DestructionSound { get; set; }

        public List<IDestroyable> DestroyablesNearby;

        public void Destroy()
        {
            Console.WriteLine($"Playing Destruction sound {DestructionSound}");
            Console.WriteLine("Create Explosion");

            foreach(IDestroyable destroyable in DestroyablesNearby)
            {
                destroyable.Destroy();
            }

        }
    }
}
