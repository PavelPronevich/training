using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ServiceLayer.DTOModels
{
    public class BaseView
    {
        public int Id { get; set; }
        protected BaseView()
        {
            Id = -1;
        }
    }
}