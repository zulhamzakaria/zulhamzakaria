using System;
using System.Collections;
using System.Collections.Generic;

namespace HashTable
{
    class Program
    {
        static void Main(string[] args)
        {
            Hashtable studentTable = new Hashtable();

            Student student1 = new Student(1, "Maria", 98);
            Student student2 = new Student(2, "John", 98);
            Student student3 = new Student(3, "Bob", 98);
            Student student4 = new Student(4, "Cleo", 98);

            studentTable.Add(student1.Id, student1);
            studentTable.Add(student2.Id, student2);
            studentTable.Add(student3.Id, student3);
            studentTable.Add(student4.Id, student4);

            Student storedStudent1 = (Student)studentTable[student1.Id];
            Console.WriteLine($"{storedStudent1.Id} {storedStudent1.Name} {storedStudent1.GPA}");
            
            foreach(DictionaryEntry entry in studentTable)
            {
                Student temp = (Student)entry.Value;
                Console.WriteLine($"{temp.Id} {temp.Name} {temp.GPA}");
            }

            foreach(Student record in studentTable.Values)
            {
                Console.WriteLine($"{record.Id} {record.Name} {record.GPA}");
            }

        }
    }
}
