using System;
using System.Collections.Generic;

namespace Delegates
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Employee> employees = new List<Employee>();
            employees.Add(new Employee() { Id = 101, Name = "Noelle", Salary = 5000M, Experience = 2 });
            employees.Add(new Employee() { Id = 102, Name = "Hillary", Salary = 5500M, Experience = 5 });
            employees.Add(new Employee() { Id = 103, Name = "Gabbie", Salary = 7000M, Experience = 7 });
            employees.Add(new Employee() { Id = 104, Name = "Siri", Salary = 1000M, Experience = 12 });

            Employee.PromoteEmployee(employees, emp => emp.Experience >= 5);
        }
    }
    delegate bool IsPromotable(Employee employee);
    class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Salary { get; set; }
        public int Experience { get; set; }

        public static void PromoteEmployee(List<Employee> employees, IsPromotable isPromotable)
        {
            foreach (var employee in employees)
            {
                if (isPromotable(employee))
                {
                    Console.WriteLine($"{employee.Name} promoted");
                }
            }
        }
    }
}
