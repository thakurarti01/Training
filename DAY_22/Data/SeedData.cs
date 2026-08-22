using Day22.Model;

namespace Day22.Data
{
    public static class SeedData
    {
        public static List<SaleLineItem> GetSales()
        {
            return new List<SaleLineItem>
            {
                new SaleLineItem { Id = 1, ProductName = "Laptop", Category = "Electronics", UnitPrice = 60000, Quantity = 2, StaffName = "Amit", StoreLocation = "Delhi", SoldAt = new DateTime(2026, 8, 22, 9, 10, 0) },
                new SaleLineItem { Id = 2, ProductName = "Phone", Category = "Electronics", UnitPrice = 30000, Quantity = 3, StaffName = "Priya", StoreLocation = "Mumbai", SoldAt = new DateTime(2026, 8, 22, 9, 25, 0) },
                new SaleLineItem { Id = 3, ProductName = "Headphones", Category = "Electronics", UnitPrice = 2500, Quantity = 5, StaffName = "Rahul", StoreLocation = "Delhi", SoldAt = new DateTime(2026, 8, 22, 9, 45, 0) },
                new SaleLineItem { Id = 4, ProductName = "Keyboard", Category = "Electronics", UnitPrice = 1500, Quantity = 4, StaffName = "Amit", StoreLocation = "Mumbai", SoldAt = new DateTime(2026, 8, 22, 10, 5, 0) },
                new SaleLineItem { Id = 5, ProductName = "Mouse", Category = "Electronics", UnitPrice = 800, Quantity = 6, StaffName = "Priya", StoreLocation = "Delhi", SoldAt = new DateTime(2026, 8, 22, 10, 20, 0) },

                new SaleLineItem { Id = 6, ProductName = "T-Shirt", Category = "Clothing", UnitPrice = 900, Quantity = 4, StaffName = "Rahul", StoreLocation = "Mumbai", SoldAt = new DateTime(2026, 8, 22, 10, 40, 0) },
                new SaleLineItem { Id = 7, ProductName = "Jeans", Category = "Clothing", UnitPrice = 1800, Quantity = 3, StaffName = "Amit", StoreLocation = "Delhi", SoldAt = new DateTime(2026, 8, 22, 11, 0, 0) },
                new SaleLineItem { Id = 8, ProductName = "Jacket", Category = "Clothing", UnitPrice = 3500, Quantity = 2, StaffName = "Priya", StoreLocation = "Mumbai", SoldAt = new DateTime(2026, 8, 22, 11, 15, 0) },
                new SaleLineItem { Id = 9, ProductName = "Shoes", Category = "Clothing", UnitPrice = 2500, Quantity = 5, StaffName = "Rahul", StoreLocation = "Delhi", SoldAt = new DateTime(2026, 8, 22, 11, 30, 0) },
                new SaleLineItem { Id = 10, ProductName = "Cap", Category = "Clothing", UnitPrice = 500, Quantity = 8, StaffName = "Amit", StoreLocation = "Mumbai", SoldAt = new DateTime(2026, 8, 22, 11, 45, 0) },

                new SaleLineItem { Id = 11, ProductName = "Apple", Category = "Grocery", UnitPrice = 150, Quantity = 10, StaffName = "Priya", StoreLocation = "Delhi", SoldAt = new DateTime(2026, 8, 22, 12, 5, 0) },
                new SaleLineItem { Id = 12, ProductName = "Milk", Category = "Grocery", UnitPrice = 70, Quantity = 12, StaffName = "Rahul", StoreLocation = "Mumbai", SoldAt = new DateTime(2026, 8, 22, 12, 20, 0) },
                new SaleLineItem { Id = 13, ProductName = "Bread", Category = "Grocery", UnitPrice = 50, Quantity = 15, StaffName = "Amit", StoreLocation = "Delhi", SoldAt = new DateTime(2026, 8, 22, 12, 35, 0) },
                new SaleLineItem { Id = 14, ProductName = "Rice", Category = "Grocery", UnitPrice = 1200, Quantity = 4, StaffName = "Priya", StoreLocation = "Mumbai", SoldAt = new DateTime(2026, 8, 22, 12, 50, 0) },
                new SaleLineItem { Id = 15, ProductName = "Oil", Category = "Grocery", UnitPrice = 160, Quantity = 8, StaffName = "Rahul", StoreLocation = "Delhi", SoldAt = new DateTime(2026, 8, 22, 13, 5, 0) },

                new SaleLineItem { Id = 16, ProductName = "Sofa", Category = "Furniture", UnitPrice = 25000, Quantity = 1, StaffName = "Amit", StoreLocation = "Mumbai", SoldAt = new DateTime(2026, 8, 22, 13, 20, 0) },
                new SaleLineItem { Id = 17, ProductName = "Chair", Category = "Furniture", UnitPrice = 4000, Quantity = 3, StaffName = "Priya", StoreLocation = "Delhi", SoldAt = new DateTime(2026, 8, 22, 13, 35, 0) },
                new SaleLineItem { Id = 18, ProductName = "Table", Category = "Furniture", UnitPrice = 7000, Quantity = 2, StaffName = "Rahul", StoreLocation = "Mumbai", SoldAt = new DateTime(2026, 8, 22, 13, 50, 0) },
                new SaleLineItem { Id = 19, ProductName = "Bed", Category = "Furniture", UnitPrice = 30000, Quantity = 1, StaffName = "Amit", StoreLocation = "Delhi", SoldAt = new DateTime(2026, 8, 22, 14, 5, 0) },
                new SaleLineItem { Id = 20, ProductName = "Lamp", Category = "Furniture", UnitPrice = 1200, Quantity = 6, StaffName = "Priya", StoreLocation = "Mumbai", SoldAt = new DateTime(2026, 8, 22, 14, 20, 0) },

                new SaleLineItem { Id = 21, ProductName = "Laptop", Category = "Electronics", UnitPrice = 60000, Quantity = 1, StaffName = "Rahul", StoreLocation = "Delhi", SoldAt = new DateTime(2026, 8, 22, 14, 35, 0) },
                new SaleLineItem { Id = 22, ProductName = "Phone", Category = "Electronics", UnitPrice = 30000, Quantity = 2, StaffName = "Amit", StoreLocation = "Mumbai", SoldAt = new DateTime(2026, 8, 22, 14, 50, 0) },
                new SaleLineItem { Id = 23, ProductName = "Headphones", Category = "Electronics", UnitPrice = 2500, Quantity = 3, StaffName = "Priya", StoreLocation = "Delhi", SoldAt = new DateTime(2026, 8, 22, 15, 5, 0) },
                new SaleLineItem { Id = 24, ProductName = "Keyboard", Category = "Electronics", UnitPrice = 1500, Quantity = 2, StaffName = "Rahul", StoreLocation = "Mumbai", SoldAt = new DateTime(2026, 8, 22, 15, 20, 0) },
                new SaleLineItem { Id = 25, ProductName = "Mouse", Category = "Electronics", UnitPrice = 800, Quantity = 10, StaffName = "Amit", StoreLocation = "Delhi", SoldAt = new DateTime(2026, 8, 22, 15, 35, 0) },

                new SaleLineItem { Id = 26, ProductName = "T-Shirt", Category = "Clothing", UnitPrice = 900, Quantity = 7, StaffName = "Priya", StoreLocation = "Mumbai", SoldAt = new DateTime(2026, 8, 22, 15, 50, 0) },
                new SaleLineItem { Id = 27, ProductName = "Jeans", Category = "Clothing", UnitPrice = 1800, Quantity = 4, StaffName = "Rahul", StoreLocation = "Delhi", SoldAt = new DateTime(2026, 8, 22, 16, 5, 0) },
                new SaleLineItem { Id = 28, ProductName = "Jacket", Category = "Clothing", UnitPrice = 3500, Quantity = 1, StaffName = "Amit", StoreLocation = "Mumbai", SoldAt = new DateTime(2026, 8, 22, 16, 20, 0) },
                new SaleLineItem { Id = 29, ProductName = "Shoes", Category = "Clothing", UnitPrice = 2500, Quantity = 3, StaffName = "Priya", StoreLocation = "Delhi", SoldAt = new DateTime(2026, 8, 22, 16, 35, 0) },
                new SaleLineItem { Id = 30, ProductName = "Cap", Category = "Clothing", UnitPrice = 500, Quantity = 5, StaffName = "Rahul", StoreLocation = "Mumbai", SoldAt = new DateTime(2026, 8, 22, 16, 50, 0) },

                new SaleLineItem { Id = 31, ProductName = "Apple", Category = "Grocery", UnitPrice = 150, Quantity = 20, StaffName = "Amit", StoreLocation = "Delhi", SoldAt = new DateTime(2026, 8, 22, 17, 5, 0) },
                new SaleLineItem { Id = 32, ProductName = "Milk", Category = "Grocery", UnitPrice = 70, Quantity = 15, StaffName = "Priya", StoreLocation = "Mumbai", SoldAt = new DateTime(2026, 8, 22, 17, 20, 0) },
                new SaleLineItem { Id = 33, ProductName = "Bread", Category = "Grocery", UnitPrice = 50, Quantity = 18, StaffName = "Rahul", StoreLocation = "Delhi", SoldAt = new DateTime(2026, 8, 22, 17, 35, 0) },
                new SaleLineItem { Id = 34, ProductName = "Rice", Category = "Grocery", UnitPrice = 1200, Quantity = 3, StaffName = "Amit", StoreLocation = "Mumbai", SoldAt = new DateTime(2026, 8, 22, 17, 50, 0) },
                new SaleLineItem { Id = 35, ProductName = "Oil", Category = "Grocery", UnitPrice = 160, Quantity = 10, StaffName = "Priya", StoreLocation = "Delhi", SoldAt = new DateTime(2026, 8, 22, 18, 5, 0) },

                new SaleLineItem { Id = 36, ProductName = "Sofa", Category = "Furniture", UnitPrice = 25000, Quantity = 1, StaffName = "Rahul", StoreLocation = "Mumbai", SoldAt = new DateTime(2026, 8, 22, 18, 20, 0) },
                new SaleLineItem { Id = 37, ProductName = "Chair", Category = "Furniture", UnitPrice = 4000, Quantity = 2, StaffName = "Amit", StoreLocation = "Delhi", SoldAt = new DateTime(2026, 8, 22, 18, 35, 0) },
                new SaleLineItem { Id = 38, ProductName = "Table", Category = "Furniture", UnitPrice = 7000, Quantity = 1, StaffName = "Priya", StoreLocation = "Mumbai", SoldAt = new DateTime(2026, 8, 22, 18, 50, 0) },
                new SaleLineItem { Id = 39, ProductName = "Bed", Category = "Furniture", UnitPrice = 30000, Quantity = 1, StaffName = "Rahul", StoreLocation = "Delhi", SoldAt = new DateTime(2026, 8, 22, 19, 5, 0) },
                new SaleLineItem { Id = 40, ProductName = "Lamp", Category = "Furniture", UnitPrice = 1200, Quantity = 4, StaffName = "Amit", StoreLocation = "Mumbai", SoldAt = new DateTime(2026, 8, 22, 19, 20, 0) }
            };
        }

        public static List<Promotion> GetPromotions()
        {
            return new List<Promotion>
            {
                new PercentOffPromotion { Code = "P10", PercentOff = 10 },
                new PercentOffPromotion { Code = "P15", PercentOff = 15 },
                new PercentOffPromotion { Code = "P20", PercentOff = 20 },
                new FlatAmountPromotion { Code = "FLAT500", AmountOff = 500 },
                new FlatAmountPromotion { Code = "FLAT1000", AmountOff = 1000 },
                new BuyOneGetOnePromotion { Code = "BOGO1" },
                new BuyOneGetOnePromotion { Code = "BOGO2" },
                new PercentOffPromotion { Code = "P30", PercentOff = 30 },
                new FlatAmountPromotion { Code = "FLAT200", AmountOff = 200 }
            };
        }
    }
}