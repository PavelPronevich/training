using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateDataBase
{
    public class Report
    {
        public int ReportID { get; set; }
        public string FileReport { get; set; }
        public int ManagerID { get; set; }
        public virtual Manager Manager { get; set; }
 
    }
}
