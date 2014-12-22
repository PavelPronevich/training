using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebLayer.Models
{
    public class ItemSelectList
    {
        public int Id {get;set;}
        public string Name{get;set;}

        public ItemSelectList(int id, string name)
        {
            this.Id = id;
            this.Name = name;
        }

    }
}