using System;

class PatientMonitoring
{
    public static Patient[] Monitor()
    {
        Console.Write("Enter number of records: ");
        int size = Convert.ToInt32(Console.ReadLine());

        Patient[] patients = new Patient[size];

        for (int i = 0; i < size; i++)
        {
            patients[i] = new Patient();

            Console.WriteLine($"\nEnter details of Record {i + 1}");

            Console.Write("Heart Rate (BPM): ");
            patients[i].HeartRate = Convert.ToInt32(Console.ReadLine());

            Console.Write("Oxygen Level (SpO2): ");
            patients[i].OxygenLevel = Convert.ToDouble(Console.ReadLine());

            Console.Write("Systolic Blood Pressure: ");
            patients[i].SystolicBP = Convert.ToInt32(Console.ReadLine());

            Console.Write("Diastolic Blood Pressure: ");
            patients[i].DiastolicBP = Convert.ToInt32(Console.ReadLine());

            Console.Write("Timestamp: ");
            patients[i].Timestamp = Console.ReadLine();
        }
        return patients;
    }

    // ------------------------------------------------------------------

    public static void CheckVitals(Patient[] patients)
    {

        for (int i = 0; i < patients.Length; i++)
        {
            Console.WriteLine($"\nRecord {i + 1} ({patients[i].Timestamp})");

            bool normal = true;

            if (patients[i].HeartRate < 60 || patients[i].HeartRate > 100)
            {
                Console.WriteLine("Heart Rate is not under control.");
                normal = false;
            }

            if (patients[i].OxygenLevel < 95 || patients[i].OxygenLevel > 100)
            {
                Console.WriteLine("Oxygen Level is not under control.");
                normal = false;
            }

            if (patients[i].SystolicBP < 90 || patients[i].SystolicBP > 120)
            {
                Console.WriteLine("Systolic Blood Pressure is not under control.");
                normal = false;
            }

            if (patients[i].DiastolicBP < 60 || patients[i].DiastolicBP > 80)
            {
                Console.WriteLine("Diastolic Blood Pressure is not under control.");
                normal = false;
            }

            if (normal)
            {
                Console.WriteLine("All vitals are under control.");
            }
        }
    }
}