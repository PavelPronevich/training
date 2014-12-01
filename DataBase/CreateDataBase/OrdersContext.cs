using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Runtime.CompilerServices;
using System.Threading;

namespace CreateDataBase
{
    public class OrdersContext :DbContext
       {
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Order> Orders { get; set; }
        public OrdersContext() : base("MyDB") { }
        
        private static Mutex mutexManager = new Mutex();
        private static Mutex mutexCustomer = new Mutex();
        private static Mutex mutexProduct = new Mutex();
        private static Mutex mutexOrder = new Mutex();
        private static Mutex mutexReport = new Mutex();
        public static void AddManagerToDB(string managerSurname)
        {
            mutexManager.WaitOne();
            if (GetManagerID(managerSurname) == null)
            {
                using (var db = new OrdersContext())
                {
                    Manager newManager = new Manager();
                    newManager.ManagerSurname = managerSurname;
                    db.Managers.Add(newManager);
                    db.SaveChanges();
                }
            }
            mutexManager.ReleaseMutex();
        }

        public static int? GetManagerID(string managerSurname)
        {
            using (var db = new OrdersContext())
            {
                if (db.Managers.FirstOrDefault(x => x.ManagerSurname == managerSurname)!=null)
                {
                    return db.Managers.FirstOrDefault(x => x.ManagerSurname == managerSurname).ManagerID;
                }
            }
            return null;
        }

        public static void AddCustomerToDB(string customerrName)
        {
            mutexCustomer.WaitOne();
            if (GetCustomerID(customerrName) == null)
            {
                using (var db = new OrdersContext())
                {
                    Customer newCustomer = new Customer();
                    newCustomer.CustomerName = customerrName;
                    db.Customers.Add(newCustomer);
                    db.SaveChanges();
                }
            }
            mutexCustomer.ReleaseMutex();
        }

        public static int? GetCustomerID(string customerName)
        {
            using (var db = new OrdersContext())
            {
                if (db.Customers.FirstOrDefault(x => x.CustomerName == customerName) != null)
                {
                    return db.Customers.FirstOrDefault(x => x.CustomerName == customerName).CustomerID;
                }
            }
            return null;
        }
        
        public static void AddProductToDB(string productName)
        {
            mutexProduct.WaitOne();
            if (GetProductID(productName) == null)
            {
                using (var db = new OrdersContext())
                {
                    Product newProduct = new Product();
                    newProduct.ProductName = productName;
                    db.Products.Add(newProduct);
                    db.SaveChanges();
                }
            }
            mutexProduct.ReleaseMutex();
        }

        public static int? GetProductID(string productName)
        {
            using (var db = new OrdersContext())
            {
                if (db.Products.FirstOrDefault(x => x.ProductName == productName) != null)
                {
                    return db.Products.FirstOrDefault(x => x.ProductName == productName).ProductID;
                }
            }
            return null;
        }

        private static void AddOrdersToDB(List<Order> orders)
        {
            mutexOrder.WaitOne();
            using (var db = new OrdersContext())
            {
                db.Orders.AddRange(orders);
                db.SaveChanges();
            }
            mutexOrder.ReleaseMutex();
        }
        
        static string GetManagerName(string fileName)
        {
            int beginNameIndex = fileName.LastIndexOf("\\") + 1;
            int endnNameIndex = fileName.LastIndexOf("_");
            return fileName.Substring(beginNameIndex, endnNameIndex - beginNameIndex);
        }

        static string GetReportName(string fileName)
        {
            int beginNameIndex = fileName.LastIndexOf("\\") + 1;
            return fileName.Substring(beginNameIndex);
        }
        static DateTime GetDateTime(string fileName)
        {
            string reportDateString = (fileName.Remove(fileName.LastIndexOf(".")).Remove(0, fileName.LastIndexOf("_") + 1));
            DateTime reportDate = new DateTime(Convert.ToInt32(reportDateString.Remove(0, 4)),
            Convert.ToInt32(reportDateString.Remove(0, 2).Remove(2)), Convert.ToInt32(reportDateString.Remove(2)));
            return reportDate;
        }

        public static bool IsReportInDB(string fileReport)
        {
            using (var db = new OrdersContext())
            {
                if (db.Reports.Count(x => x.FileReport == fileReport) == 0)
                    return false;
                else return true;
            }
        }
        public static bool AddReportToDB(string fileReport, int managerID)
        {
            bool result;
            mutexReport.WaitOne();
            if (!IsReportInDB(fileReport))
                using (var db = new OrdersContext())
                {
                    db.Reports.Add(new Report(){FileReport=fileReport,ManagerID=managerID});
                    db.SaveChanges();
                    result = true;
                }
            else result = false;
            mutexReport.ReleaseMutex();
            return result;
        }

        public static void AddOrdersToDBFromFile(string fileName)
        {
            string fileReport = GetReportName(fileName);
            if (!IsReportInDB(fileName))
            {

                string managerName = GetManagerName(fileName);
                if (GetManagerID(managerName) == null)
                    AddManagerToDB(managerName);
                int managerID = (int)GetManagerID(managerName);
                if (AddReportToDB(fileReport, managerID))
                {
                    DateTime reportDate = GetDateTime(fileName);
                    IEnumerable<string> strings = System.IO.File.ReadLines(fileName);
                    List<Order> orders = new List<Order>();
                    foreach (var item in strings)
                    {
                        Order order = new Order();
                        order.ManagerID = managerID;
                        order.ReportDate = reportDate;
                        List<int> breaks = new List<int>();
                        breaks.Add(item.IndexOf(','));
                        breaks.Add(item.IndexOf(',', breaks[0] + 1));
                        breaks.Add(item.IndexOf(',', breaks[1] + 1));
                        DateTime orderDate = System.Convert.ToDateTime(item.Substring(0, breaks[0]));
                        order.OrderDate = orderDate;
                        string customerName = item.Substring(breaks[0] + 1, breaks[1] - breaks[0] - 1);
                        int customerID;
                        if (GetCustomerID(customerName) == null)
                        {
                            AddCustomerToDB(customerName);
                        }
                        customerID = (int)GetCustomerID(customerName);
                        order.CustomerID = customerID;
                        string productName = item.Substring(breaks[1] + 1, breaks[2] - breaks[1] - 1);
                        int productID;
                        if (GetProductID(productName) == null)
                        {
                            AddProductToDB(productName);
                        }
                        productID = (int)GetProductID(productName);
                        order.ProductID = productID;
                        double price = System.Convert.ToDouble(item.Substring(breaks[2] + 1));
                        order.Price = price;
                        orders.Add(order);
                    }
                    AddOrdersToDB(orders);
                    Console.WriteLine("Data from {0} added to the database.", fileName);
                }
            }
        }

        public static void DreadfulDayCame(bool isItTrue)
        {
            if (isItTrue)
            {
                using (var db = new OrdersContext())
                {
                    var q = from b in db.Products
                            select b;
                    var q1 = from b in db.Managers
                             select b;
                    var q2 = from b in db.Customers
                             select b;
                    var q3 = from b in db.Orders
                             select b;
                    var q4 = from b in db.Reports
                             select b;
                    db.Products.RemoveRange(q);
                    db.Managers.RemoveRange(q1);
                    db.Customers.RemoveRange(q2);
                    db.Orders.RemoveRange(q3);
                    db.Reports.RemoveRange(q4);
                    db.SaveChanges();
                    Console.WriteLine("All data deleted from the database.");
                }
            }
        }

        public static void RemoveDataRfomDB(string fileName)
        {
            mutexReport.WaitOne();
            if (IsReportInDB(GetReportName(fileName)))
            {
                string reportName = GetReportName(fileName);
                using (var db = new OrdersContext())
                {
                    Report report = db.Reports.FirstOrDefault(x => x.FileReport == reportName);
                    if (report != null)
                        db.Reports.Remove(report);
                    db.SaveChanges();
                    mutexReport.ReleaseMutex();
                }

                mutexOrder.WaitOne();
                mutexCustomer.WaitOne();
                mutexManager.WaitOne();
                mutexProduct.WaitOne();
                string managerName = GetManagerName(fileName);
                DateTime reportDate = GetDateTime(fileName);
                if (GetManagerID(managerName) != null)
                {
                    int managerID = (int)GetManagerID(managerName);
                    List<int> productsID = new List<int>();
                    List<int> customersID = new List<int>();
                    using (var db = new OrdersContext())
                    {
                        var q = from b in db.Orders
                                where (b.ManagerID == managerID && b.ReportDate == reportDate)
                                select b;
                        foreach (var item in q)
                        {
                            if (!productsID.Contains(item.ProductID))
                                productsID.Add(item.ProductID);
                            if (!customersID.Contains(item.CustomerID))
                                customersID.Add(item.CustomerID);
                        }
                        db.Orders.RemoveRange(q);
                        db.SaveChanges();
                        foreach (var item in productsID)
                        {
                            if (db.Orders.Count(x => x.ProductID == item) == 0)
                            {
                                db.Products.Remove(db.Products.FirstOrDefault(x => x.ProductID == item));
                            }
                        }
                        foreach (var item in customersID)
                        {
                            if (db.Orders.Count(x => x.CustomerID == item) == 0)
                            {
                                db.Customers.Remove(db.Customers.FirstOrDefault(x => x.CustomerID == item));
                            }
                        }
                        if (db.Orders.Count(x => x.ManagerID == managerID) == 0)
                        {
                            db.Managers.Remove(db.Managers.FirstOrDefault(x => x.ManagerID == managerID));
                        }
                        db.SaveChanges();
                    }
                }
                mutexCustomer.ReleaseMutex();
                mutexManager.ReleaseMutex();
                mutexOrder.ReleaseMutex();
                mutexProduct.ReleaseMutex();
                Console.WriteLine("Data from {0} removed from the database.", fileName);
            }
            else mutexReport.ReleaseMutex();
           
        }
    }
}
