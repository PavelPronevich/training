using System.Collections.Generic;

namespace DBLayer
{
    public class Product : IDbEntity
    {
        public int Id { get; set; }
        public string ProductName { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
    }
}
