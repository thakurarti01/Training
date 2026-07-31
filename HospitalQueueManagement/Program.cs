using System;
using System.Collections.Generic;

Queue<string> patients = new Queue<string>();

while (true)
{
    Console.WriteLine("1. Register Patient");
    Console.WriteLine("2. Call Next Patient");
    Console.WriteLine("3. View Next Patient");
    Console.WriteLine("4. Display Waiting Patients");
    Console.WriteLine("5. Search Patient");
    Console.WriteLine("6. Count Waiting Patients");
    Console.WriteLine("7. Exit");

    Console.Write("\nEnter Choice : ");
    int choice = Convert.ToInt32(Console.ReadLine());

    switch (choice)
    {
        case 1:
            Console.Write("Enter Patient Name : ");
            string name = Console.ReadLine();
            patients.Enqueue(name);
            Console.WriteLine("Patient Registered Successfully.");
            break;

        case 2:
            if (patients.Count == 0)
            {
                Console.WriteLine("No patients waiting.");
            }
            else
            {
                Console.WriteLine("Calling Patient : " + patients.Dequeue());
            }
            break;

        case 3:
            if (patients.Count == 0)
            {
                Console.WriteLine("No patients waiting.");
            }
            else
            {
                Console.WriteLine("Next Patient : " + patients.Peek());
            }
            break;

        case 4:
            if (patients.Count == 0)
            {
                Console.WriteLine("No patients waiting.");
            }
            else
            {
                Console.WriteLine("Waiting Patients:");
                foreach (string patient in patients)
                {
                    Console.WriteLine(patient);
                }
            }
            break;

        case 5:
            Console.Write("Enter Patient Name to Search : ");
            string search = Console.ReadLine();

            if (patients.Contains(search))
                Console.WriteLine("Patient Found.");
            else
                Console.WriteLine("Patient Not Found.");
            break;

        case 6:
            Console.WriteLine("Waiting Patients Count : " + patients.Count);
            break;

        case 7:
            Console.WriteLine("Exiting...");
            return;

        default:
            Console.WriteLine("Invalid Choice.");
            break;
    }
}