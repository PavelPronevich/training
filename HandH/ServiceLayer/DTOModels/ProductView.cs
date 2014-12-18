using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ServiceLayer.DTOModels
{
    public class ProductView : BaseView
    {
        [Display(Name = "Product")]
        public string Name { get; set; }
    }
}