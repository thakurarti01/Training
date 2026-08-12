using System;

// Base class is NOT sealed
public class TaxCalculator
{
    // Virtual method can be overridden by child classes
    public virtual decimal CalculateTax(decimal amount)
    {
        // Basic tax rate = 10%
        return amount * 0.1m;
    }
}

// RegionalTaxCalculator inherits from TaxCalculator
public class RegionalTaxCalculator : TaxCalculator
{
    // sealed means this override cannot be overridden again
    public sealed override decimal CalculateTax(decimal amount)
    {
        // Regional tax rate = 12%
        return amount * 0.12m;
    }
}

/*
    This class is NOT allowed to override CalculateTax
    because the method was sealed in RegionalTaxCalculator.

    Uncommenting this code will produce a compiler error:

    public class InvalidTaxCalculator : RegionalTaxCalculator
    {
        public override decimal CalculateTax(decimal amount)
        {
            return amount * 0.15m;
        }
    }

    Error:
    Cannot override inherited member because it is sealed.
*/


// This entire class is sealed
// Therefore, no other class can inherit from it
public sealed class FixedDiscountCalculator
{
    // Applies a 10% discount
    public decimal ApplyDiscount(decimal price)
    {
        return price * 0.9m;
    }
}

/*
    This class cannot inherit from FixedDiscountCalculator
    because FixedDiscountCalculator is sealed.

    Uncommenting this code will produce a compiler error:

    public class InvalidDiscountCalculator : FixedDiscountCalculator
    {
    }

    Error:
    Cannot derive from sealed type 'FixedDiscountCalculator'
*/


class Program
{
    static void Main()
    {
        // We can still create and use a sealed method normally
        RegionalTaxCalculator regional =
            new RegionalTaxCalculator();

        decimal tax = regional.CalculateTax(200);

        Console.WriteLine(
            $"RegionalTaxCalculator.CalculateTax(200) -> {tax:F2}"
        );


        // We can also create and use a sealed class normally
        FixedDiscountCalculator discount =
            new FixedDiscountCalculator();

        decimal discountedPrice =
            discount.ApplyDiscount(50);

        Console.WriteLine(
            $"FixedDiscountCalculator.ApplyDiscount(50) -> {discountedPrice:F2}"
        );
    }
}