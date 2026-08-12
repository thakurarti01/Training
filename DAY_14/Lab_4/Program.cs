using System;

// Interface for basic vehicle functionality
public interface IVehicle
{
    // Read-only property
    string Model { get; }

    // Every vehicle must have Drive()
    void Drive();
}

// Interface for electric vehicle functionality
public interface IElectric
{
    // Property can be read and changed
    int BatteryPercent { get; set; }

    // Every electric vehicle must have Charge()
    void Charge();
}

// Combined interface
// It inherits members from BOTH IVehicle and IElectric
public interface IElectricVehicle : IVehicle, IElectric
{
    // Nothing new is added here
    // It simply combines both interfaces
}

// ElectricCar must implement all members
// inherited from IElectricVehicle
public class ElectricCar : IElectricVehicle
{
    // Model can only be set during object creation
    public string Model { get; init; }

    // Private backing field for battery
    private int _batteryPercent;

    // BatteryPercent property with 0-100 validation
    public int BatteryPercent
    {
        get
        {
            return _batteryPercent;
        }

        set
        {
            // Clamp value between 0 and 100
            if (value < 0)
                _batteryPercent = 0;
            else if (value > 100)
                _batteryPercent = 100;
            else
                _batteryPercent = value;
        }
    }

    // Drive reduces battery by 10%
    public void Drive()
    {
        BatteryPercent -= 10;

        // Make sure battery never goes below 0
        if (BatteryPercent < 0)
            BatteryPercent = 0;
    }

    // Charge sets battery to 100%
    public void Charge()
    {
        BatteryPercent = 100;
    }
}

class Program
{
    static void Main()
    {
        // Create an ElectricCar with 100% battery
        ElectricCar car = new ElectricCar
        {
            Model = "Tesla Model 3",
            BatteryPercent = 100
        };

        // Drive three times
        car.Drive();
        Console.WriteLine($"Battery after drive 1: {car.BatteryPercent}%");

        car.Drive();
        Console.WriteLine($"Battery after drive 2: {car.BatteryPercent}%");

        car.Drive();
        Console.WriteLine($"Battery after drive 3: {car.BatteryPercent}%");

        // Charge the car
        car.Charge();
        Console.WriteLine($"Battery after charge: {car.BatteryPercent}%");

        // Store the same object in an IVehicle variable
        IVehicle vehicle = car;

        // We can access IVehicle members
        Console.WriteLine(
            $"As IVehicle - model: {vehicle.Model}"
        );

        // Store the same object in an IElectric variable
        IElectric electric = car;

        // We can access IElectric members
        Console.WriteLine(
            $"As IElectric - BatteryPercent: {electric.BatteryPercent}%"
        );
    }
}