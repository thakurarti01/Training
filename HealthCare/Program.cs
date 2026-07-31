using System;

class Program
{
    static void Main()
    {
        Patient[] patients = PatientMonitoring.Monitor();

        PatientMonitoring.CheckVitals(patients);
    }
}