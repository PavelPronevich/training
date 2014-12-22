using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using ServiceLayer.DTOModels;

namespace ServiceLayer.DTOStat
{
    public class ProductStat:ProductView
    {

        [DisplayFormat(DataFormatString = "{0:P3}")]
        [Display(Name = "Percentage")]
        public double Percentage { get; set; }

        [Display(Name = "Total Order Price")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public double TotalOrderPrice { get; set; }

        [Display(Name = "Average Order Price")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public double AverageOrderPrice { get; set; }

        [Display(Name = "Average Order Price Per Day")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public double AverageOrderPricePerDay { get; set; }

    }
}