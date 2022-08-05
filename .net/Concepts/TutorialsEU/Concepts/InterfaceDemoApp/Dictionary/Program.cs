using System;
using System.Collections.Generic;

namespace Dictionary
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<int, string> dict = new Dictionary<int, string>();
            dict.Add(1, "kiln");
            dict.Add(2, "horses");

            Dictionary<int, string> dict2 = new Dictionary<int, string>()
            {
                {1, "kilt" },
                {2, "mangoes" }
            };

            Employee[] employees =
            {
                new Employee("Gwyn",1, 920f),
                new Employee("Pearson",2, 90f),
                new Employee("Tom",3, 250f),
            };

            Dictionary<string, Employee> employeesDirectory = new Dictionary<string, Employee>();
            foreach (var item in employees)
            {
                employeesDirectory.Add(item.Name, item);
            }

            string key = "Tom";
            if (employeesDirectory.ContainsKey(key))
            {
                Employee emp = employeesDirectory[key];
                Console.WriteLine($"{emp.Name} {emp.EmployeeId} {emp.Rate} {emp.Salary}");
            }


        }
    }

    class Employee
    {
        public string Name { get; set; }
        public int EmployeeId { get; set; }
        public float Rate { get; set; }
        public float Salary
        {
            get
            {
                return 30 * Rate;
            }
        }
        public Employee(string name, int employeeId, float rate)
        {
            this.Name = name;
            this.EmployeeId = employeeId;
            this.Rate = rate;
        }

    }
}
