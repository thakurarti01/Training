using System; 
public class Employee 
{
    private decimal _salary; 
    public string Name = string.Empty; 
    protected string Department = "General"; 
    internal string EmployeeId = "E-101"; 
    protected internal void SetDepartment(string department) 
    {
        Department = department; 
    } 
    public void ShowSalary() 
    { 
        Console.WriteLine($"Salary: {_salary}"); 
    } 
    private protected void AdjustSalary(decimal salary) 
    { 
        _salary = salary; 
    } 
} 
public class Manager : Employee 
{
    public void PrintDetails() 
    { 
        Name = "Benhar"; 
        SetDepartment("Engineer"); 
        AdjustSalary(50000); 
        Console.WriteLine($"Name: {Name}"); 
        Console.WriteLine($"Department: {Department}"); 
        Console.WriteLine($"Employee ID: {EmployeeId}"); 
        ShowSalary(); 
    } 
} 
class Program 
{ 
    static void Main(string[] args) 
    { 
        Manager manager = new Manager(); 
        manager.PrintDetails(); 
        Console.ReadLine(); 
    } 
}