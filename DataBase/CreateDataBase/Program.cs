using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateDataBase
{
        
    class Program
    {

        static void Main(string[] args)
        {
            
            //string path = @"E:\1\DB.log";
            //if (!File.Exists(path))
            //{
            //    using (FileStream fs = File.Create(path)) { }
            //}

            OrdersContext.DreadfulDayCame(true);
            Console.WriteLine(OrdersContext.GetManagerID("dasdsds"));
            //OrdersContext.GetManagers();
            //OrdersContext.GetProducts();
            //OrdersContext.GetCustomers();
            /*
            // @"E:\1\Lopital_02112014.csv", 
            string[] file = new string[] { @"E:\1\Kopernik_20122014.csv", @"E:\1\Lopital_02112014.csv", @"E:\1\Kopernik_23122014.csv" };
            Parallel.ForEach(file, item => OrdersContext.AddOrdersToDBFromFile(item));

            Console.ReadKey();
            OrdersContext.RemoveDataRfomDB(@"E:\1\Lopital_02112014.csv");

            
           // foreach (var item in file)
           // {
           //     OrdersContext.AddOrdersToDBFromFile(item);
           // }
           
            /*OrdersContext.GetManagers();
            OrdersContext.GetProducts();
            OrdersContext.GetCustomers();

            OrdersContext.AddProductToDB("Radio");
            OrdersContext.AddProductToDB("Laptop");
            OrdersContext.AddProductToDB("Laptop");
            OrdersContext.AddProductToDB("Table");

            OrdersContext.AddCustomerToDB("Abramson");
            OrdersContext.AddCustomerToDB("Jeff");
            OrdersContext.AddCustomerToDB("Kennedy");
            OrdersContext.AddCustomerToDB("Audley");
            OrdersContext.AddCustomerToDB("Jeff");
            */

            

            //foreach (var item in OrdersContext.CustomersInDB)
            //    Console.WriteLine("{0} {1}", item.CustomerName, item.CustomerID);

            //foreach (var item in OrdersContext.ProductInDB)
            //    Console.WriteLine("{0} {1}",item.ProductName,item.ProductID);
            
            //foreach(var item in OrdersContext.ManagersInDB)
            //    Console.WriteLine("{0} {1}",item.ManagerSurname,item.ManagerID);
                


            //    Console.ReadKey();
           
        }
    }
}
