using Day22.Data;
using Day22.Reports;

namespace Day22
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // ---------------------------------------------
            // 1. Seed data
            // ---------------------------------------------

            var sales = SeedData.GetSales();
            var promotions = SeedData.GetPromotions();

            var report = new SalesReport(sales, promotions);

            Console.WriteLine("==============================================");
            Console.WriteLine("       INSIGHTDESK SALES ANALYTICS");
            Console.WriteLine("==============================================");

            Console.WriteLine($"Total Sales: {sales.Count}");
            Console.WriteLine($"Total Promotions: {promotions.Count}");

            // ---------------------------------------------
            // 2. Top Selling Products
            // ---------------------------------------------

            Console.WriteLine("\n==============================================");
            Console.WriteLine("1. TOP SELLING PRODUCTS");
            Console.WriteLine("==============================================");

            var topProducts = report.TopSellingProducts(5);

            foreach (var item in topProducts)
            {
                Console.WriteLine(
                    $"{item.ProductName} - Quantity Sold: {item.TotalQuantity}"
                );
            }

            // ---------------------------------------------
            // 3. Revenue By Category
            // ---------------------------------------------

            Console.WriteLine("\n==============================================");
            Console.WriteLine("2. REVENUE BY CATEGORY");
            Console.WriteLine("==============================================");

            // Store the query first.
            var revenueByCategory = report.RevenueByCategory();

            // Additional operation happens before enumeration.
            var staffForDeferredDemo = report.StaffPerformanceReport().ToList();

            Console.WriteLine("Staff report executed before RevenueByCategory.");

            foreach (var item in revenueByCategory)
            {
                Console.WriteLine(
                    $"{item.Category} - Revenue: ₹{item.Revenue:N2}"
                );
            }

            // ---------------------------------------------
            // 4. Staff Performance
            // ---------------------------------------------

            Console.WriteLine("\n==============================================");
            Console.WriteLine("3. STAFF PERFORMANCE");
            Console.WriteLine("==============================================");

            var staffReport = report.StaffPerformanceReport();

            foreach (var staff in staffReport)
            {
                Console.WriteLine(
                    $"{staff.StaffName} | " +
                    $"Sales Count: {staff.TotalSalesCount} | " +
                    $"Revenue: ₹{staff.TotalRevenue:N2} | " +
                    $"Average Sale: ₹{staff.AverageSaleValue:N2}"
                );
            }

            // ---------------------------------------------
            // 5. Hourly Sales Trend
            // ---------------------------------------------

            Console.WriteLine("\n==============================================");
            Console.WriteLine("4. HOURLY SALES TREND");
            Console.WriteLine("==============================================");

            // Store query before enumeration.
            var hourlyTrend = report.HourlySalesTrend();

            // Another operation happens first.
            var topProductsForHourly = report.TopSellingProducts(3).ToList();

            Console.WriteLine("Top products calculated before HourlySalesTrend.");

            foreach (var hour in hourlyTrend)
            {
                Console.WriteLine(
                    $"{hour.Hour}:00 | " +
                    $"Sales: {hour.SaleCount} | " +
                    $"Revenue: ₹{hour.Revenue:N2}"
                );
            }

            // ---------------------------------------------
            // 6. Percent-Off Promotions
            // ---------------------------------------------

            Console.WriteLine("\n==============================================");
            Console.WriteLine("5. PERCENT-OFF PROMOTIONS ABOVE 15%");
            Console.WriteLine("==============================================");

            var promotionsOver15 = report.PercentOffPromotionsOver(15);

            foreach (var promotion in promotionsOver15)
            {
                Console.WriteLine(
                    $"{promotion.Code} - {promotion.PercentOff}% OFF"
                );
            }

            // ---------------------------------------------
            // 7. Low Performing Categories
            // ---------------------------------------------

            Console.WriteLine("\n==============================================");
            Console.WriteLine("6. LOW PERFORMING CATEGORIES");
            Console.WriteLine("==============================================");

            var lowCategories = report.LowPerformingCategories(10000);

            foreach (var category in lowCategories)
            {
                Console.WriteLine(
                    $"{category.Category} - Revenue: ₹{category.Revenue:N2}"
                );
            }

            // ---------------------------------------------
            // 8. Store Comparison
            // ---------------------------------------------

            Console.WriteLine("\n==============================================");
            Console.WriteLine("7. STORE COMPARISON");
            Console.WriteLine("==============================================");

            var storeReport = report.StoreComparisonReport();

            foreach (var store in storeReport)
            {
                Console.WriteLine(
                    $"{store.StoreLocation} | " +
                    $"Revenue: ₹{store.Revenue:N2} | " +
                    $"Items: {store.ItemCount} | " +
                    $"Top Category: {store.TopCategory} | " +
                    $"Top Category Revenue: ₹{store.TopCategoryRevenue:N2}"
                );
            }

            // ---------------------------------------------
            // 9. Deferred vs Snapshot
            // ---------------------------------------------

            Console.WriteLine("\n==============================================");
            Console.WriteLine("8. DEFERRED VS SNAPSHOT");
            Console.WriteLine("==============================================");

            report.DeferredVsSnapshotDemo();

            // ---------------------------------------------
            // 10. Syntax Equivalence Check
            // ---------------------------------------------

            Console.WriteLine("\n==============================================");
            Console.WriteLine("SYNTAX EQUIVALENCE CHECK");
            Console.WriteLine("==============================================");

            report.SyntaxEquivalenceCheck();

            // ---------------------------------------------
            // 11. Broken Staff Sort
            // ---------------------------------------------

            Console.WriteLine("\n==============================================");
            Console.WriteLine("BROKEN VS CORRECT STAFF SORT");
            Console.WriteLine("==============================================");

            Console.WriteLine("\nBROKEN VERSION:");

            var brokenSort = report.BrokenStaffSort();

            foreach (var staff in brokenSort)
            {
                Console.WriteLine(
                    $"{staff.StaffName} - Revenue: ₹{staff.TotalRevenue:N2}"
                );
            }

            Console.WriteLine("\nCORRECT VERSION:");

            var correctSort = report.StaffPerformanceReport();

            foreach (var staff in correctSort)
            {
                Console.WriteLine(
                    $"{staff.StaffName} - Revenue: ₹{staff.TotalRevenue:N2}"
                );
            }

            Console.WriteLine(
                "\nExplanation: OrderBy().OrderBy() replaces the first ordering. " +
                "Use ThenBy() when adding a secondary sort."
            );

            // ---------------------------------------------
            // 12. Edge Case 1
            // TopN larger than available products
            // ---------------------------------------------

            Console.WriteLine("\n==============================================");
            Console.WriteLine("EDGE CASE 1: TOP 100 PRODUCTS");
            Console.WriteLine("==============================================");

            var top100 = report.TopSellingProducts(100);

            foreach (var product in top100)
            {
                Console.WriteLine(
                    $"{product.ProductName} - {product.TotalQuantity}"
                );
            }

            Console.WriteLine(
                "\nHandled successfully. Only available products are returned."
            );

            // ---------------------------------------------
            // 13. Edge Case 2
            // No promotions above 999%
            // ---------------------------------------------

            Console.WriteLine("\n==============================================");
            Console.WriteLine("EDGE CASE 2: PROMOTIONS ABOVE 999%");
            Console.WriteLine("==============================================");

            var noPromotions = report.PercentOffPromotionsOver(999).ToList();

            if (noPromotions.Count == 0)
            {
                Console.WriteLine("No matching promotions found.");
            }
            else
            {
                foreach (var promotion in noPromotions)
                {
                    Console.WriteLine(
                        $"{promotion.Code} - {promotion.PercentOff}%"
                    );
                }
            }

            Console.WriteLine("\n==============================================");
            Console.WriteLine("          PROGRAM COMPLETED");
            Console.WriteLine("==============================================");

            Console.ReadLine();
        }
    }
}