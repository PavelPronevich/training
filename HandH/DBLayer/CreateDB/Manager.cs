using System.Collections.Generic;

namespace DBLayer
{
    public class Manager : IDbEntity
    {
        public int Id { get; set; }
        public string ManagerSurname { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Report> Reports { get; set; }

        
    }
}
