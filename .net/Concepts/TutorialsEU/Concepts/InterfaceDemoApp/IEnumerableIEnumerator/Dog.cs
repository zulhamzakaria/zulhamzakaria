using System;

namespace IEnumerableIEnumerator
{
    class Dog
    {
        public string Name { get; set; }
        public bool IsNaughty { get; set; }

        public Dog(string name, bool isNaughty)
        {
            Name = name;
            IsNaughty = IsNaughty;
        }

        public void GiveTreat(int numberOfTreats)
        {
            Console.WriteLine($"Dog: {Name} barks {numberOfTreats} times!");
        }
    }
}
