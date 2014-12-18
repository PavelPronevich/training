using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace ServiceLayer.DTOModels
{
    public class BaseView
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }
        protected BaseView()
        {
            Id = -1;
        }
    }
}