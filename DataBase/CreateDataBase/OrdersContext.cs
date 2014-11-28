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
        public DbSet<Product> Goods { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public OrdersContext() : base("MyDB") { }

        public static List<Customer> CustomersInDB = new List<Customer>();
        public static List<Product> ProductInDB = new List<Product>();
        public static List<Manager> ManagersInDB = new List<Manager>();
        
        
        //private static Object thisLock = new Object();
        private static Mutex mutexManager = new Mutex();
        private static Mutex mutexCustomer = new Mutex();
        private static Mutex mutexProduct = new Mutex();
        private static Mutex mutexOrder = new Mutex();
        
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

        //[MethodImpl(MethodImplOptions.Synchronized)]
        public static void AddManagerToDB(string managerSurname)
        {
            mutexManager.WaitOne();
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
            mutexManager.ReleaseMutex();
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

        //[MethodImpl(MethodImplOptions.Synchronized)]
        public static void AddCustomerToDB(string customerrName)
        {
            mutexCustomer.WaitOne();
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
            mutexCustomer.ReleaseMutex();
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

        //[MethodImpl(MethodImplOptions.Synchronized)]
        public static void AddProductToDB(string productName)
        {
            mutexProduct.WaitOne();
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
            mutexProduct.ReleaseMutex();
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

        //[MethodImpl(MethodImplOptions.Synchronized)]
        private static void AddOrdersToDB(List<Order> orders)
        {
            mutexOrder.WaitOne();
            //lock (thisLock)
            {
                using (var db = new OrdersContext())
                {
                    db.Orders.AddRange(orders);
                    db.SaveChanges();
                }
            }
            mutexOrder.ReleaseMutex();
        }
        
        public static string GetManagerName(string fileName)
        {
            int beginNameIndex = fileName.LastIndexOf("\\") + 1;
            int endnNameIndex = fileName.LastIndexOf("_");
            return fileName.Substring(beginNameIndex, endnNameIndex - beginNameIndex);
        }
        public static DateTime GetDateTime(string fileName)
        {
            string reportDateString = (fileName.Remove(fileName.LastIndexOf(".")).Remove(0, fileName.LastIndexOf("_") + 1));
            DateTime reportDate = new DateTime(Convert.ToInt32(reportDateString.Remove(0, 4)),
            Convert.ToInt32(reportDateString.Remove(0, 2).Remove(2)), Convert.ToInt32(reportDateString.Remove(2)));
            return reportDate;
        }

        
        //public static void AddOrdersToDBFromFile(string fileName, string logFileName)
        public static void AddOrdersToDBFromFile(string fileName)
        {
            string managerName = GetManagerName(fileName); 
            int managerID;
            if (GetManagerID(managerName) == null)
            {
                AddManagerToDB(managerName);
            }
            managerID = (int)GetManagerID(managerName);
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
                double price= System.Convert.ToDouble(item.Substring(breaks[2] + 1));
                order.Price = price;
                orders.Add(order);
            }
             AddOrdersToDB(orders);
            
             
             //System.IO.StreamWriter writer = new System.IO.StreamWriter(logFileName, true);
             //writer.WriteLine("{0};{1}", fileName, DateTime.Now);
             //writer.Close();

             /*using (System.IO.StreamReader sr = System.IO.File.OpenText(logFileName))
             {
                 string s = "";
                 while ((s = sr.ReadLine()) != null)
                 {
                     Console.WriteLine(s);
                 }
             }
              */
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

        [MethodImpl(MethodImplOptions.Synchronized)]
        public static void RemoveDataRfomDB(string fileName)
        {
            mutexOrder.WaitOne();
            mutexCustomer.WaitOne();
            mutexManager.WaitOne();
            mutexProduct.WaitOne();
            string managerName=GetManagerName(fileName);
            DateTime reportDate=GetDateTime(fileName);
            int managerID=(int)GetManagerID(managerName);
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
                

                foreach(var item in productsID)
                {
                    
                    if (db.Orders.Count(x => x.ProductID == item) == 0)
                    {
                        
                        db.Goods.Remove(db.Goods.FirstOrDefault(x=>x.ProductID==item));
                        ProductInDB.Remove(ProductInDB.FirstOrDefault(x => x.ProductID == item));
                    }
                }
                foreach (var item in customersID)
                {
                    if (db.Orders.Count(x => x.CustomerID == item) == 0)
                    {
                        db.Customers.Remove(db.Customers.FirstOrDefault(x => x.CustomerID == item));
                        CustomersInDB.Remove(CustomersInDB.FirstOrDefault(x => x.CustomerID == item));
                    }
                }
                if (db.Orders.Count(x => x.ManagerID == managerID) == 0)
                {
                    db.Managers.Remove(db.Managers.FirstOrDefault(x => x.ManagerID == managerID));
                    ManagersInDB.Remove(ManagersInDB.FirstOrDefault(x => x.ManagerID == managerID));
                }
               
                db.SaveChanges();
            }
            mutexCustomer.ReleaseMutex();
            mutexManager.ReleaseMutex();
            mutexOrder.ReleaseMutex();
            mutexProduct.ReleaseMutex();
            
        }
    }
}
