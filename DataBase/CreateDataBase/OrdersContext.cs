using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace CreateDataBase
{
    public class OrdersContext :DbContext
    {
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Product> Goods { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Order> Orders { get; set; }

        public OrdersContext() : base("MyDB") { }
    }
}
