using System.Data.Entity;

namespace DBLayer
{
    public class OrdersContext :DbContext
    {
        public DbSet<Manager> Managers { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Customer> Customers { get; set; }
        public DbSet<Report> Reports { get; set; }
        public DbSet<Order> Orders { get; set; }
        public OrdersContext() : base("MyDB") { }

        
    }
}
