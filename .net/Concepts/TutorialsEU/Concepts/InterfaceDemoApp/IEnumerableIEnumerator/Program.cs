using System;
using System.Collections.Generic;

namespace IEnumerableIEnumerator
{
    class Program
    {

        static void Main(string[] args)
        {
            //DogShelter dogShelter = new DogShelter();

            //foreach (Dog dog in dogShelter)
            //{
            //    if (!dog.IsNaughty) { dog.GiveTreat(2); }
            //    else { dog.GiveTreat(1); }
            //}

            //IEnumerable<int> unknownCollection;
            //unknownCollection = GetCollection(1);

            //Console.WriteLine("This is a List<int>");
            //foreach(var num in unknownCollection)
            //{
            //    Console.Write(num + " ");
            //}

            List<int> numbersList = new List<int>() { 1, 2, 3, 4 };
            int[] numbersArray = new int[] { 1, 7, 1, 3 };

            CollectionSum(numbersArray);
            foreach(var item in numbersArray)
            {
                Console.Write("-");
            }
            Console.WriteLine("");
            CollectionSum(numbersList);


        }

        static void CollectionSum(IEnumerable<int> anyCollection)
        {
            int sum = 0;
            foreach(int num in anyCollection)
            {
                sum += num;
            }
            Console.WriteLine($"Sum is {sum}");
        }

        static IEnumerable<int> GetCollection(int option)
        {

            List<int> numbersList = new List<int>() { 1, 2, 3, 4, 5 };
            Queue<int> numbersQ = new Queue<int>();
            numbersQ.Enqueue(6);
            numbersQ.Enqueue(7);
            numbersQ.Enqueue(8);
            numbersQ.Enqueue(9);

            switch (option)
            {
                case 1:
                    return numbersList;
                case 2:
                    return numbersQ;
                default:
                    return new int[] { 11, 12, 13, 14, 15 };
            }
        }
    }
}
