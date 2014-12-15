using System;

namespace DBLayer
{
    public class Order : IDbEntity
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set;} 
        public double Price {get;set;}
        public DateTime ReportDate { get; set; }
        public int ManagerID { get; set; }
        public int ProductID { get; set; }
        public int CustomerID { get; set; }
        public virtual Manager Manager { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Product Product { get; set; }
        
        
    }
}
