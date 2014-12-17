using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceLayer.DTOModels
{
    public class OrderView : BaseView
    {
        public DateTime OrderDate { get; set; }
        public double Price { get; set; }
        public int ManagerID { get; set; }
        public string ManagerName { get; set; }
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
    }
}