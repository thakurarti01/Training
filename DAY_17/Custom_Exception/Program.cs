using System;

// User-defined exception
class InsufficientBalanceException : Exception
{
    public InsufficientBalanceException(string message) : base(message)
    {
    }
}

class Program
{
    static void Main()
    {
        try
        {
            double balance = 5000;
            double withdraw = 7000;

            if (withdraw > balance)
            {
                throw new InsufficientBalanceException(
                    "Insufficient balance!"
                );
            }

            balance = balance - withdraw;

            Console.WriteLine("Withdrawal successful.");
            Console.WriteLine("Remaining balance: " + balance);
        }
        catch (InsufficientBalanceException ex)
        {
            Console.WriteLine("Exception: " + ex.Message);
        }
    }
}