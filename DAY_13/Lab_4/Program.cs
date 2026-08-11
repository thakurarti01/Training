using System;

public abstract class Employee
{
    public string Name { get; }
    public decimal BaseSalary { get; }

    // Constructor
    protected Employee(string name, decimal baseSalary)
    {
        Name = name;
        BaseSalary = baseSalary;
    }

    // Abstract method
    public abstract decimal CalculatePay();

    // Concrete method
    public void PrintPaySlip()
    {
        Console.WriteLine($"{Name}: {CalculatePay():C2}");
    }
}

// Salaried Employee
public class SalariedEmployee : Employee
{
    public SalariedEmployee(string name, decimal baseSalary)
        : base(name, baseSalary)
    {
    }

    // Override abstract method
    public override decimal CalculatePay()
    {
        return BaseSalary;
    }
}

// Commission Employee
public class CommissionEmployee : Employee
{
    public decimal CommissionEarned;

    public CommissionEmployee(
        string name,
        decimal baseSalary,
        decimal commission)
        : base(name, baseSalary)
    {
        CommissionEarned = commission;
    }

    // Override abstract method
    public override decimal CalculatePay()
    {
        return BaseSalary + CommissionEarned;
    }
}

class Program
{
    static void Main()
    {
        // Base class references containing different subclasses
        Employee[] employees =
        {
            new SalariedEmployee("Alice", 4500m),
            new CommissionEmployee("Bob", 3000m, 200m),
            new CommissionEmployee("Carla", 3500m, 650m)
        };

        // Polymorphism
        foreach (Employee employee in employees)
        {
            employee.PrintPaySlip();
        }

        // This would NOT compile because Employee is abstract:
        // Employee employee = new Employee("David", 4000m);
    }
}