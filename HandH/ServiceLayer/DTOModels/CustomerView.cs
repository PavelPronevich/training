using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ServiceLayer.DTOModels
{
    public class CustomerView : BaseView
    {
        [Display(Name = "Customer")]
        public string Name { get; set; }
    }
}