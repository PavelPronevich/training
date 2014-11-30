using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateDataBase
{
    public class Manager
    {
        public int ManagerID { get; set; }
        public string ManagerSurname { get; set; }
        public virtual ICollection<Order> Orders { get; set; }
        public virtual ICollection<Report> Reports { get; set; }

        
    }
}
