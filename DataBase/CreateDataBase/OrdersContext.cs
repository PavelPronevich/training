using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Runtime.CompilerServices;

namespace CreateDataBase
{
        

    public class OrdersContext :DbContext
    {
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Product> Goods { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public OrdersContext() : base("MyDB") { }

        public static List<Customer> CustomersInDB = new List<Customer>();
        public static List<Product> ProductInDB = new List<Product>();
        public static List<Manager> ManagersInDB = new List<Manager>();
        
        
        private static Object thisLock = new Object();
        
        public static void GetManagers()
        {
            ManagersInDB.Clear();
            using (var db = new OrdersContext())
            {
                var q= from b in db.Managers
                                select b;
                foreach(var item in q)
                {
                    ManagersInDB.Add(item);
                }
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]public static void AddManagerToDB(string managerSurname)
        {
            //lock (thisLock)
            {
                if (GetManagerID(managerSurname) == null)
                {
                    using (var db = new OrdersContext())
                    {
                        Manager newManager = new Manager();
                        newManager.ManagerSurname = managerSurname;
                        db.Managers.Add(newManager);
                        db.SaveChanges();
                        ManagersInDB.Add(newManager);
                    }
                }
            }
        }

        public static int? GetManagerID(string managerSurname)
        {
            foreach (var item in ManagersInDB)
            {
                if (item.ManagerSurname.ToLower() == managerSurname.ToLower())
                return item.ManagerID; 
            }
            return null;
        }

        public static void GetCustomers()
        {
            CustomersInDB.Clear();
            using (var db = new OrdersContext())
            {
                var q = from b in db.Customers
                        select b;
                foreach (var item in q)
                {
                    CustomersInDB.Add(item);
                }
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]public static void AddCustomerToDB(string customerrName)
        {
            //lock (thisLock)
            {
                if (GetCustomerID(customerrName) == null)
                {
                    using (var db = new OrdersContext())
                    {
                        Customer newCustomer = new Customer();
                        newCustomer.CustomerName = customerrName;
                        db.Customers.Add(newCustomer);
                        db.SaveChanges();
                        CustomersInDB.Add(newCustomer);
                    }
                }
            }
        }

        public static int? GetCustomerID(string customerrName)
        {
            foreach (var item in CustomersInDB)
            {
                if (item.CustomerName.ToLower() == customerrName.ToLower())
                    return item.CustomerID;
            }
            return null;
        }
        public static void GetProducts()
        {
            ProductInDB.Clear();
            using (var db = new OrdersContext())
            {
                var q = from b in db.Goods
                        select b;
                foreach (var item in q)
                {
                    ProductInDB.Add(item);
                }
            }
        }

        [MethodImpl(MethodImplOptions.Synchronized)]public static void AddProductToDB(string productName)
        {
        //lock (thisLock)
            {
                if (GetProductID(productName) == null)
                {
                    using (var db = new OrdersContext())
                    {
                        Product newProduct = new Product();
                        newProduct.ProductName = productName;
                        db.Goods.Add(newProduct);
                        db.SaveChanges();
                        ProductInDB.Add(newProduct);
                    }
                }
            }
        }

        public static int? GetProductID(string productName)
        {
            foreach (var item in ProductInDB)
            {
                if (item.ProductName.ToLower() == productName.ToLower())
                    return item.ProductID;
            }
            return null;
        }

        [MethodImpl(MethodImplOptions.Synchronized)]private static void AddOrdersToDB(List<Order> orders)
        {
            //lock (thisLock)
            {
                using (var db = new OrdersContext())
                {
                    db.Orders.AddRange(orders);
                    db.SaveChanges();
                }
            }
        }
        public static void AddOrdersToDBFromFile(string fileName,string logFileName)
        {

            int beginNameIndex = fileName.LastIndexOf("\\") + 1;
            int endnNameIndex = fileName.LastIndexOf("_");
            string managerName=fileName.Substring(beginNameIndex, endnNameIndex - beginNameIndex);
            int managerID;
            if (GetManagerID(managerName) == null)
            {
                AddManagerToDB(managerName);
            }
            managerID = (int)GetManagerID(managerName);
            string reportDateString = (fileName.Remove(fileName.LastIndexOf(".")).Remove(0, fileName.LastIndexOf("_") + 1));
            DateTime reportDate = new DateTime(Convert.ToInt32(reportDateString.Remove(0, 4)),
            Convert.ToInt32(reportDateString.Remove(0, 2).Remove(2)), Convert.ToInt32(reportDateString.Remove(2)));
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
                double price= System.Convert.ToDouble(item.Substring(breaks[2] + 1));
                order.Price = price;
                orders.Add(order);
            }
             AddOrdersToDB(orders);
             
            System.IO.StreamWriter writer = new System.IO.StreamWriter(logFileName, true);
             writer.WriteLine("Данные из файла {0} добавлены в базу данных {1}", fileName, DateTime.Now);
             writer.Close();

             using (System.IO.StreamReader sr = System.IO.File.OpenText(logFileName))
             {
                 string s = "";
                 while ((s = sr.ReadLine()) != null)
                 {
                     Console.WriteLine(s);
                 }
             }
        }

        public static void DreadfulDayCame(bool isItTrue)
        {
            if (isItTrue)
            {
                using (var db = new OrdersContext())
                {
                    var q = from b in db.Goods
                            select b;
                    var q1 = from b in db.Managers
                             select b;
                    var q2 = from b in db.Customers
                             select b;
                    var q3 = from b in db.Orders
                             select b;
                    db.Goods.RemoveRange(q);
                    db.Managers.RemoveRange(q1);
                    db.Customers.RemoveRange(q2);
                    db.Orders.RemoveRange(q3);
                    db.SaveChanges();
                    ProductInDB.Clear();
                    CustomersInDB.Clear();
                    ManagersInDB.Clear();
                }
            }
        }
        
    }
}
