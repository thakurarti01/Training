using System; 
public enum OrderStatus 
{ 
    Pending = 0, 
    Shipped = 1, 
    Delivered = 2, 
    Canceled = 3 
} 
class Program 
{ 
    static void Main(string[] args) 
    {
         OrderStatus status = OrderStatus.Shipped; 
         Console.WriteLine(status); 
         Console.WriteLine((int)status); 
         Console.WriteLine(Enum.GetName(typeof(OrderStatus), 2)); 
    } 
}