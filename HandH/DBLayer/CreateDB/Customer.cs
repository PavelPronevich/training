using System.Collections.Generic;

namespace DBLayer
{
    public class Customer : IDbEntity
    {
        public int Id { get; set; }
        public string CustomerName { get; set; }
        public virtual ICollection<Order> Orders { get; set; }

       
    }
}
