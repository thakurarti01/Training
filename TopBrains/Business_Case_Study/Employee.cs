using System;

class Employee
{
    public int Id;
    public string Name;
    public string Designation;
    public string Department;
    public int ManagerId;

    public Employee(int id, string name, string designation,
                    string department, int managerId)
    {
        Id = id;
        Name = name;
        Designation = designation;
        Department = department;
        ManagerId = managerId;
    }
}