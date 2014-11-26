using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateDataBase
{
        
    class Program
    {


        static void Main(string[] args)
        {
            //OrdersContext.DreadfulDayCame(true);
            OrdersContext.GetManagers();
            foreach (var item in OrdersContext.ManagersInDB)
            { Console.WriteLine("{0}, {1}",item.ManagerSurname,item.ManagerID); }
            OrdersContext.GetProducts();
            OrdersContext.GetCustomers();
            OrdersContext.AddOrdersToDBFromFile(@"E:\1\Lopital_02112014.csv");
           
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

            

            foreach (var item in OrdersContext.CustomersInDB)
                Console.WriteLine("{0} {1}", item.CustomerName, item.CustomerID);

            //foreach (var item in OrdersContext.ProductInDB)
            //    Console.WriteLine("{0} {1}",item.ProductName,item.ProductID);
            
            //foreach(var item in OrdersContext.ManagersInDB)
            //    Console.WriteLine("{0} {1}",item.ManagerSurname,item.ManagerID);
                


                Console.ReadKey();
           
        }
    }
}
