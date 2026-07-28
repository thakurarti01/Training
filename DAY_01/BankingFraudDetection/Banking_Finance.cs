using System;

class Banking_Finance
{
    public static void Service()
    {
        Console.Write("Enter number of transactions: ");
        int size = Convert.ToInt32(Console.ReadLine());

        Transaction[] transactions = new Transaction[size];

        for (int i = 0; i < size; i++)
        {
            transactions[i] = new Transaction();

            Console.WriteLine($"Enter details for transaction {i + 1}:");

            Console.Write("Account No: ");
            transactions[i].AccountNo = Console.ReadLine();

            Console.Write("Enter Amount: ");
            transactions[i].Amount = Convert.ToDecimal(Console.ReadLine());

            Console.Write("Enter Timestamp: ");
            transactions[i].Timestamp = DateTime.Parse(Console.ReadLine());

            Console.Write("Enter Merchant Name: ");
            transactions[i].MerchantName = Console.ReadLine();
        }

        HighValueTransaction(transactions);
        MultipleTransactions(transactions);
        AbnormalTransaction(transactions);
    }

//---------------------------------------------------------------------------------------------------------------
    public static void HighValueTransaction(Transaction[] transactions)
    {
        const decimal threshold = 5000000m;
        bool fraud = false;

        for(int i = 0; i < transactions.Length; i++)
        {
            if (transactions[i].Amount > threshold)
            {
                Console.WriteLine($"High value transaction detected: Account No: {transactions[i].AccountNo}, Amount: {transactions[i].Amount}, Timestamp: {transactions[i].Timestamp}, Merchant Name: {transactions[i].MerchantName}");
                fraud = true;
            }
        }

        if(!fraud)
        {
            Console.WriteLine("No high value transactions detected.");
        }
    }
 //------------------------------------------------------------------------------------------
    public static void MultipleTransactions(Transaction[] transactions)
    {
        const decimal threshold = 5000000m;
        bool fraud = false;

        for(int i = 0; i < transactions.Length; i++)
        {
            int count = 1;

            for(int j = i + 1; j < transactions.Length; j++)
            {
                if(transactions[i].AccountNo == transactions[j].AccountNo && 
                   transactions[i].Amount > threshold &&
                   transactions[j].Amount > threshold)
                {
                    count++;
                }
            }

            if(count >= 3)
            {
                fraud = true;
                Console.WriteLine("Fraud Detected!");
                Console.WriteLine($"Account No : {transactions[i].AccountNo}");
                Console.WriteLine($"High Value Transactions : {count}");
            }
        }
        if (!fraud)
        {
            Console.WriteLine("No suspicious multiple high-value transactions found.");
        }
    }

//---------------------------------------------------------------------------------------------------------------
    public static void AbnormalTransaction(Transaction[] transactions)
    {
        const decimal threshold = 5000000m;
        bool fraud = false;
        for (int i = 0; i < transactions.Length; i++)
        {
            int count = 1;

            for (int j = i + 1; j < transactions.Length; j++)
            {
                if (transactions[i].MerchantName == transactions[j].MerchantName &&
                    transactions[i].Amount > threshold &&
                    transactions[j].Amount > threshold)
                {
                    count++;
                }
            }

            if (count >= 2)
            {
                fraud = true;

                Console.WriteLine("Abnormal Transaction Detected!");
                Console.WriteLine($"Merchant Name : {transactions[i].MerchantName}");
                Console.WriteLine($"High Value Transactions : {count}");
            }
        }

        if (!fraud)
        {
            Console.WriteLine("No abnormal transaction pattern found.");
        }
    }
}