using System;
using System.Text;

class Lab4
{
    const string rawData = @"
john smith|engineering|72000
MARY jones|sales|65000
ravi KUMARI|engineering|81000
";

    static int appendCount = 0;

    static void Append(StringBuilder sb, string text)
    {
        sb.Append(text);
        appendCount++;
    }

    static void AppendLine(StringBuilder sb, string text)
    {
        sb.AppendLine(text);
        appendCount++;
    }

    static void Main()
    {
        // Split into rows and skip blank rows
        string[] rows = rawData.Split(
            new[] { '\r', '\n' }, //split whenever we encounter a line-break character
            StringSplitOptions.RemoveEmptyEntries
        );

        StringBuilder report = new StringBuilder();

        decimal totalSalary = 0;
        int employeeCount = 0;

        // Title
        AppendLine(report, "EMPLOYEE COMPENSATION REPORT");
        AppendLine(report, "");

        // Header
        Append(report, "Name".PadRight(20));
        Append(report, "Department".PadRight(20));
        AppendLine(report, "Salary".PadLeft(10));

        // Process employees
        foreach (string row in rows)
        {
            // Split on |
            string[] fields = row.Split('|');

            if (fields.Length != 3)
                continue;

            string name = fields[0].Trim();
            string department = fields[1].Trim();
            string salaryText = fields[2].Trim();

            // Normalize name using Lab 3 method
            name = StringToolkit.ToTitleCase(name);

            // Normalize department
            department = StringToolkit.ToTitleCase(department);

            decimal salary = decimal.Parse(salaryText);

            totalSalary += salary;
            employeeCount++;

            // Build aligned line
            Append(report, name.PadRight(20));
            Append(report, department.PadRight(20));
            AppendLine(report, salary.ToString("N0").PadLeft(10));
        }

        // Footer
        AppendLine(report, "");
        AppendLine(
            report,
            "Total Salary: " + totalSalary.ToString("N0")
        );

        AppendLine(
            report,
            "Employees: " + employeeCount
        );

        // Print report
        Console.WriteLine(report.ToString());

        // Print statistics
        Console.WriteLine();
        Console.WriteLine("StringBuilder Append calls: " + appendCount);
        Console.WriteLine("String concatenations in loop: 0");
    }
}