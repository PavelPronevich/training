using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace ServiceLayer.DTOModels
{
    public class ManagerView:BaseView
    {
        [Display(Name = "Manager")]
        public string Name { get; set; }
    }
}