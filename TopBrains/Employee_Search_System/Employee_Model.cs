using System;

class Employee_Model
{
    public int Id{get; set;}
    public string Name{get; set;}
    public string Department{get; set;}
    public string Designation{get; set;}
    public int Exp{get; set;}
    public int Salary{get; set;}
    public string City{get; set;}

    public Employee_Model(int id, string name, string designation, string department, int exp, int salary, string city)
    {
        Id = id;
        Name = name;
        Department = department;
        Designation = designation;
        Exp = exp;
        Salary = salary;
        City = city;
        
    }

    public void Display()
    {
        Console.WriteLine("**********************************");
        Console.WriteLine("ID          : " + Id);
        Console.WriteLine("Name        : " + Name);
        Console.WriteLine("Department  : " + Department);
        Console.WriteLine("Designation : " + Designation);
        Console.WriteLine("Experience  : " + Exp + " Years");
        Console.WriteLine("Salary      : " + Salary);
        Console.WriteLine("City        : " + City);
    }
    
}