using System;
using System.Collections.Generic;
using System.Linq;

class Program
{
    static void Main()
    {
        List<Employee_Model> employees = new List<Employee_Model>
        {
            new Employee_Model(1001,"Rahul Sharma","IT","Software Engineer",2,45000,"Chennai"),
            new Employee_Model(1002,"Priya Singh","HR","HR Executive",3,40000,"Bangalore"),
            new Employee_Model(1003,"Amit Kumar","Finance","Accountant",5,55000,"Hyderabad"),
            new Employee_Model(1004,"Neha Patel","IT","Senior Developer",6,85000,"Pune"),
            new Employee_Model(1005,"Arjun Reddy","Sales","Sales Executive",2,38000,"Chennai"),
            new Employee_Model(1006,"Sneha Iyer","Marketing","Marketing Executive",4,52000,"Coimbatore"),
            new Employee_Model(1007,"Karan Mehta","IT","Team Lead",8,95000,"Mumbai"),
            new Employee_Model(1008,"Divya Nair","Support","Support Engineer",1,32000,"Kochi"),
            new Employee_Model(1009,"Rohit Verma","IT","Software Engineer",3,50000,"Delhi"),
            new Employee_Model(1010,"Anjali Gupta","Finance","Financial Analyst",4,65000,"Noida"),
            new Employee_Model(1011,"Suresh Kumar","Admin","Administrator",7,58000,"Madurai"),
            new Employee_Model(1012,"Pooja Sharma","HR","Recruiter",2,42000,"Bangalore"),
            new Employee_Model(1013,"Vikram Das","IT","System Engineer",5,62000,"Chennai"),
            new Employee_Model(1014,"Meena Joshi","Support","Technical Support",3,41000,"Trichy"),
            new Employee_Model(1015,"Naveen Raj","Sales","Sales Manager",9,98000,"Salem"),
            new Employee_Model(1016,"Kavya R","Marketing","SEO Analyst",2,45000,"Chennai"),
            new Employee_Model(1017,"Ajay Kumar","IT","DevOps Engineer",4,72000,"Hyderabad"),
            new Employee_Model(1018,"Lakshmi Devi","Finance","Senior Accountant",6,76000,"Coimbatore"),
            new Employee_Model(1019,"Manoj Singh","IT","QA Engineer",3,53000,"Pune"),
            new Employee_Model(1020,"Deepika Rao","HR","HR Manager",8,90000,"Bangalore")
        };

        while (true)
        {
            Console.WriteLine("\n**********************************");
            Console.WriteLine("      ABC Technologies");
            Console.WriteLine(" Employee Search Management System");
            Console.WriteLine("************************************");

            Console.WriteLine("1. Display All Employees");
            Console.WriteLine("2. Search by Employee ID (Linear Search)");
            Console.WriteLine("3. Search by Employee ID (Binary Search)");
            Console.WriteLine("4. Search by Employee Name");
            Console.WriteLine("5. Search by Department");
            Console.WriteLine("6. Search by City");
            Console.WriteLine("7. Search by Experience");
            Console.WriteLine("8. Search by Salary Range");
            Console.WriteLine("9. Exit");

            Console.Write("\nEnter your choice: ");
            int choice = Convert.ToInt32(Console.ReadLine());

            switch (choice)
            {
                case 1:
                    DisplayAll(employees);
                    break;

                case 2:
                    Console.Write("Enter Employee ID: ");
                    int id = Convert.ToInt32(Console.ReadLine());
                    LinearSearch(employees, id);
                    break;

                case 3:
                    Console.Write("Enter Employee ID: ");
                    int id2 = Convert.ToInt32(Console.ReadLine());
                    BinarySearch(employees, id2);
                    break;

                case 4:
                    Console.Write("Enter Name: ");
                    SearchByName(employees, Console.ReadLine());
                    break;

                case 5:
                    Console.Write("Enter Department: ");
                    SearchByDepartment(employees, Console.ReadLine());
                    break;

                case 6:
                    Console.Write("Enter City: ");
                    SearchByCity(employees, Console.ReadLine());
                    break;

                case 7:
                    Console.Write("Enter Experience (Years): ");
                    int exp = Convert.ToInt32(Console.ReadLine());
                    SearchByExperience(employees, exp);
                    break;

                case 8:
                    Console.Write("Minimum Salary: ");
                    double min = Convert.ToDouble(Console.ReadLine());

                    Console.Write("Maximum Salary: ");
                    double max = Convert.ToDouble(Console.ReadLine());

                    SearchBySalary(employees, min, max);
                    break;

                case 9:
                    return;

                default:
                    Console.WriteLine("Invalid Choice");
                    break;
            }
        }
    }

    // Display all employees
    static void DisplayAll(List<Employee_Model> employees)
    {
        foreach (Employee_Model emp in employees)
            emp.Display();
    }

    // Linear Search
    static void LinearSearch(List<Employee_Model> employees, int id)
    {
        foreach (Employee_Model emp in employees)
        {
            if (emp.Id == id)
            {
                emp.Display();
                return;
            }
        }

        Console.WriteLine("Employee Not Found.");
    }

    // Binary Search
    static void BinarySearch(List<Employee_Model> employees, int id)
    {
        employees = employees.OrderBy(e => e.Id).ToList();

        int low = 0;
        int high = employees.Count - 1;

        while (low <= high)
        {
            int mid = (low + high) / 2;

            if (employees[mid].Id == id)
            {
                employees[mid].Display();
                return;
            }
            else if (id < employees[mid].Id)
            {
                high = mid - 1;
            }
            else
            {
                low = mid + 1;
            }
        }

        Console.WriteLine("Employee Not Found.");
    }

    // Search by Name
    static void SearchByName(List<Employee_Model> employees, string name)
    {
        bool found = false;

        foreach (Employee_Model emp in employees)
        {
            if (emp.Name.ToLower().Contains(name.ToLower()))
            {
                emp.Display();
                found = true;
            }
        }

        if (!found)
            Console.WriteLine("Employee Not Found.");
    }

    // Search by Department
    static void SearchByDepartment(List<Employee_Model> employees, string dept)
    {
        bool found = false;

        foreach (Employee_Model emp in employees)
        {
            if (emp.Department.Equals(dept, StringComparison.OrdinalIgnoreCase))
            {
                emp.Display();
                found = true;
            }
        }

        if (!found)
            Console.WriteLine("No Employee Found.");
    }

    // Search by City
    static void SearchByCity(List<Employee_Model> employees, string city)
    {
        bool found = false;

        foreach (Employee_Model emp in employees)
        {
            if (emp.City.Equals(city, StringComparison.OrdinalIgnoreCase))
            {
                emp.Display();
                found = true;
            }
        }

        if (!found)
            Console.WriteLine("No Employee Found.");
    }

    // Search by Experience
    static void SearchByExperience(List<Employee_Model> employees, int exp)
    {
        bool found = false;

        foreach (Employee_Model emp in employees)
        {
            if (emp.Exp >= exp)
            {
                emp.Display();
                found = true;
            }
        }

        if (!found)
            Console.WriteLine("No Employee Found.");
    }

    // Search by Salary Range
    static void SearchBySalary(List<Employee_Model> employees, double min, double max)
    {
        bool found = false;

        foreach (Employee_Model emp in employees)
        {
            if (emp.Salary >= min && emp.Salary <= max)
            {
                emp.Display();
                found = true;
            }
        }

        if (!found)
            Console.WriteLine("No Employee Found.");
    }
}