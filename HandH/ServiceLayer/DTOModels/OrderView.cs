using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ServiceLayer.DTOModels
{
    public class OrderView : BaseView
    {
        [Required]
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public double Price { get; set; }
        
        [Required]
        public int ManagerID { get; set; }
        
        [Display(Name = "Manager")]
        public string ManagerName { get; set; }
        
        [Required]
        public int ProductID { get; set; }
        [Display(Name = "Product")]

        public string ProductName { get; set; }
        
        [Required]
        public int CustomerID { get; set; }
        
        [Display(Name = "Customer")]
        public string CustomerName { get; set; }
    }
}