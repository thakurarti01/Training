using System;

class Employee
{
    public int Id;
    public string Name;
    public string Department;
    public string Designation;
    public int Experience;
    public double Salary;
    public string City;

    public Employee(int id, string name, string department,
                    string designation, int experience,
                    double salary, string city)
    {
        Id = id;
        Name = name;
        Department = department;
        Designation = designation;
        Experience = experience;
        Salary = salary;
        City = city;
    }

    // Display employee details
    public void Display()
    {
        Console.WriteLine("--------------------------------------------");
        Console.WriteLine("ID          : " + Id);
        Console.WriteLine("Name        : " + Name);
        Console.WriteLine("Department  : " + Department);
        Console.WriteLine("Designation : " + Designation);
        Console.WriteLine("Experience  : " + Experience + " Years");
        Console.WriteLine("Salary      : " + Salary);
        Console.WriteLine("City        : " + City);
    }
}