using System;
using System.Collections.Generic;

namespace Kudvenkat
{
    class Program
    {
        static void Main(string[] args)
        {

            List<Employee> employeesList = new List<Employee>()
                {
                    new Employee{Id = 101, Name = "John"},
                    new Employee{Id = 102, Name = "Bob"},
                    new Employee{Id = 103, Name = "Quinn"}
                };

            //Predicate<Employee> predicate = new Predicate<Employee>(FindEmployee);
            Employee employees = employeesList.Find(delegate (Employee emp)
            {
                return emp.Id == 102;
            });
            Console.WriteLine($"id: {employees.Id}, name: {employees.Name}");

        }
        // public static bool FindEmployee(Employee emp)
        // {
        //     return emp.Id == 102;
        // }

    }

    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
