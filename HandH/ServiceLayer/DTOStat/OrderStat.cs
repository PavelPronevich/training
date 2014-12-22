using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using ServiceLayer.DTOModels;

namespace ServiceLayer.DTOStat
{
    public class OrderStat: OrderView
    {
        [DisplayFormat(DataFormatString = "{0:P3}")]
        public double Percentage { get; set; }

    }
}