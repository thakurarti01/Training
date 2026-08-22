namespace Day22.Model
{
    public class SaleLineItem
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public string Category { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
        public string StaffName { get; set; }
        public string StoreLocation { get; set; }
        public DateTime SoldAt { get; set; }

        public decimal LineTotal => UnitPrice * Quantity;
    }
}