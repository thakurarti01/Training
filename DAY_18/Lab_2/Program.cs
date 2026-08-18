// ### Lab 2 — `List<T>` CRUD + Sorting

// Build a small **Student Roster** manager.

// 1. Define a `Student` class: `Id (int)`, `Name (string)`, `Marks (double)`.
// 2. Store students in a `List<Student>`.
// 3. Implement:
//    - `AddStudent(Student s)`
//    - `RemoveStudent(int id)`
//    - `UpdateMarks(int id, double newMarks)`
//    - `GetTopStudent()` — returns the student with highest marks
// 4. Implement custom sorting two ways:
//    - `list.Sort(...)` using a lambda `Comparison<Student>`
//    - A separate `IComparer<Student>` class `ByNameComparer`
// 5. Print the roster sorted by marks (descending) and then by name (ascending).

// **Deliverable:** Console app demonstrating all operations with clear printed output before/after each step.

// ---


using System;
using System.Collections.Generic;

// Student class represents one student in our roster.
class Student
{
    public int Id { get; set; }
    public string Name { get; set; }
    public double Marks { get; set; }

    // Constructor initializes all student details.
    public Student(int id, string name, double marks)
    {
        Id = id;
        Name = name;
        Marks = marks;
    }

    // Converts student information into readable text.
    public override string ToString()
    {
        return $"ID: {Id}, Name: {Name}, Marks: {Marks}";
    }
}


// Custom comparer used to sort students by name.
class ByNameComparer : IComparer<Student>
{
    public int Compare(Student x, Student y)
    {
        // Compare names alphabetically.
        return string.Compare(
            x.Name,
            y.Name,
            StringComparison.OrdinalIgnoreCase);
    }
}


// StudentRoster manages the List<Student>.
class StudentRoster
{
    // List<Student> stores multiple Student objects.
    private List<Student> students = new List<Student>();


    // Adds a new student to the roster.
    public void AddStudent(Student student)
    {
        students.Add(student);
    }


    // Removes a student using their ID.
    public void RemoveStudent(int id)
    {
        // Find() searches for the student whose ID matches.
        Student student = students.Find(s => s.Id == id);

        if (student != null)
        {
            students.Remove(student);
            Console.WriteLine("Student removed successfully.");
        }
        else
        {
            Console.WriteLine("Student not found.");
        }
    }


    // Updates the marks of a student using their ID.
    public void UpdateMarks(int id, double newMarks)
    {
        Student student = students.Find(s => s.Id == id);

        if (student != null)
        {
            student.Marks = newMarks;
            Console.WriteLine("Marks updated successfully.");
        }
        else
        {
            Console.WriteLine("Student not found.");
        }
    }


    // Returns the student having the highest marks.
    public Student GetTopStudent()
    {
        // Return null if the roster is empty.
        if (students.Count == 0)
        {
            return null;
        }

        Student topStudent = students[0];

        // Compare every student's marks with the current highest.
        foreach (Student student in students)
        {
            if (student.Marks > topStudent.Marks)
            {
                topStudent = student;
            }
        }

        return topStudent;
    }


    // Displays all students in the current order.
    public void Display()
    {
        foreach (Student student in students)
        {
            Console.WriteLine(student);
        }
    }


    // Sorts students by marks in descending order.
    public void SortByMarks()
    {
        // Lambda expression provides the sorting rule.
        // s2.Marks compared with s1.Marks gives descending order.
        students.Sort(
            (s1, s2) => s2.Marks.CompareTo(s1.Marks));
    }


    // Sorts students by name in ascending order.
    public void SortByName()
    {
        // Use the separate IComparer implementation.
        students.Sort(new ByNameComparer());
    }
}


class Program
{
    static void Main()
    {
        // Create the student roster.
        StudentRoster roster = new StudentRoster();


        // =====================================================
        // 1. Add Students
        // =====================================================

        Console.WriteLine("===== ADDING STUDENTS =====");

        roster.AddStudent(
            new Student(1, "Arti", 85.5));

        roster.AddStudent(
            new Student(2, "Rahul", 92.0));

        roster.AddStudent(
            new Student(3, "Neha", 78.5));

        roster.AddStudent(
            new Student(4, "Aman", 88.0));

        roster.Display();


        // =====================================================
        // 2. Update Marks
        // =====================================================

        Console.WriteLine("\n===== UPDATING MARKS =====");

        // Update Rahul's marks from 92 to 95.
        roster.UpdateMarks(2, 95.0);

        roster.Display();


        // =====================================================
        // 3. Remove Student
        // =====================================================

        Console.WriteLine("\n===== REMOVING STUDENT =====");

        // Remove student whose ID is 3.
        roster.RemoveStudent(3);

        roster.Display();


        // =====================================================
        // 4. Find Top Student
        // =====================================================

        Console.WriteLine("\n===== TOP STUDENT =====");

        Student topStudent = roster.GetTopStudent();

        if (topStudent != null)
        {
            Console.WriteLine(topStudent);
        }


        // =====================================================
        // 5. Sort by Marks
        // =====================================================

        Console.WriteLine("\n===== SORTED BY MARKS =====");

        // Sort marks from highest to lowest using a lambda.
        roster.SortByMarks();

        roster.Display();


        // =====================================================
        // 6. Sort by Name
        // =====================================================

        Console.WriteLine("\n===== SORTED BY NAME =====");

        // Sort names alphabetically using IComparer<Student>.
        roster.SortByName();

        roster.Display();
    }
}