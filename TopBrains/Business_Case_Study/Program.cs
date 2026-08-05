using System;
using System.Collections.Generic;

class Program
{
    static List<Employee> employees = new List<Employee>
    {
        new Employee(1001, "John Smith", "CEO", "Management", 0),
        new Employee(1002, "Michael Johnson", "IT Manager", "IT", 1001),
        new Employee(1003, "Sarah Williams", "HR Manager", "HR", 1001),
        new Employee(1004, "David Brown", "Finance Manager", "Finance", 1001),
        new Employee(1005, "Robert Davis", "Team Lead", "IT", 1002),
        new Employee(1006, "Jennifer Miller", "QA Lead", "IT", 1002),
        new Employee(1007, "William Wilson", "Senior Developer", "IT", 1005),
        new Employee(1008, "Emma Moore", "Senior Developer", "IT", 1005),
        new Employee(1009, "Daniel Taylor", "QA Engineer", "IT", 1006),
        new Employee(1010, "Sophia Anderson", "QA Engineer", "IT", 1006),
        new Employee(1011, "James Thomas", "Recruiter", "HR", 1003),
        new Employee(1012, "Olivia Jackson", "Recruiter", "HR", 1003),
        new Employee(1013, "Benjamin White", "Accountant", "Finance", 1004),
        new Employee(1014, "Charlotte Harris", "Accountant", "Finance", 1004),
        new Employee(1015, "Lucas Martin", "Developer", "IT", 1007),
        new Employee(1016, "Ethan Walker", "Developer", "IT", 1007),
        new Employee(1017, "Mia Hall", "UI Developer", "IT", 1008),
        new Employee(1018, "Alexander Young", "Business Analyst", "IT", 1005),
        new Employee(1019, "Harper King", "HR Executive", "HR", 1011),
        new Employee(1020, "Jack Scott", "Finance Executive", "Finance", 1013)
    };

    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\n======================================");
            Console.WriteLine("ABC TECHNOLOGIES");
            Console.WriteLine("Organization Hierarchy Management System");
            Console.WriteLine("======================================");

            Console.WriteLine("1. Display Complete Organization Chart");
            Console.WriteLine("2. Find Employee by ID");
            Console.WriteLine("3. Find Employee by Name");
            Console.WriteLine("4. Display Employees under a Manager");
            Console.WriteLine("5. Count Total Employees under a Manager");
            Console.WriteLine("6. Display Hierarchy Level");
            Console.WriteLine("7. Exit");

            Console.Write("Enter Choice: ");
            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    DisplayHierarchy(1001, "");
                    break;

                case 2:
                    SearchById();
                    break;

                case 3:
                    SearchByName();
                    break;

                case 4:
                    EmployeesUnderManager();
                    break;

                case 5:
                    CountEmployees();
                    break;

                case 6:
                    HierarchyLevel();
                    break;

                case 7:
                    return;

                default:
                    Console.WriteLine("Invalid Choice");
                    break;
            }
        }
    }

    // Recursive Organization Chart
    static void DisplayHierarchy(int managerId, string space)
    {
        foreach (Employee emp in employees)
        {
            if (emp.Id == managerId)
            {
                Console.WriteLine(space + emp.Name + " (" + emp.Designation + ")");
            }
        }

        foreach (Employee emp in employees)
        {
            if (emp.ManagerId == managerId)
            {
                DisplayHierarchy(emp.Id, space + "   ");
            }
        }
    }

    static void SearchById()
    {
        Console.Write("Enter Employee ID: ");
        int id = Convert.ToInt32(Console.ReadLine());

        foreach (Employee emp in employees)
        {
            if (emp.Id == id)
            {
                Console.WriteLine(emp.Name);
                Console.WriteLine(emp.Designation);
                Console.WriteLine(emp.Department);
                return;
            }
        }

        Console.WriteLine("Employee Not Found");
    }

    static void SearchByName()
    {
        Console.Write("Enter Name: ");
        string name = Console.ReadLine().ToLower();

        foreach (Employee emp in employees)
        {
            if (emp.Name.ToLower().Contains(name))
            {
                Console.WriteLine(emp.Id + " " + emp.Name);
            }
        }
    }

    static void EmployeesUnderManager()
    {
        Console.Write("Enter Manager ID: ");
        int id = Convert.ToInt32(Console.ReadLine());

        foreach (Employee emp in employees)
        {
            if (emp.ManagerId == id)
            {
                Console.WriteLine(emp.Name);
            }
        }
    }

    static int Count(int managerId)
    {
        int total = 0;

        foreach (Employee emp in employees)
        {
            if (emp.ManagerId == managerId)
            {
                total++;

                total += Count(emp.Id);
            }
        }

        return total;
    }

    static void CountEmployees()
    {
        Console.Write("Enter Manager ID: ");
        int id = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Total Employees = " + Count(id));
    }

    static int Level(int id)
    {
        Employee emp = null;

        foreach (Employee e in employees)
        {
            if (e.Id == id)
            {
                emp = e;
                break;
            }
        }

        if (emp == null)
            return -1;

        if (emp.ManagerId == 0)
            return 0;

        return 1 + Level(emp.ManagerId);
    }

    static void HierarchyLevel()
    {
        Console.Write("Enter Employee ID: ");
        int id = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Hierarchy Level = " + Level(id));
    }
}