using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using ServiceLayer.DTOModels;

namespace ServiceLayer.DTOStat
{
    public class CustomerStat:CustomerView
    {
        [Display(Name = "Percentage")]
        public double Percentage { get; set; }
    }
}