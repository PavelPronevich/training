using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneStation
{
    static public class Time
    {
        public static DateTime ProgramTime = DateTime.Now;
        public static DateTime Now
        {
            get
            {
               return ProgramTime.AddMilliseconds((DateTime.Now.Ticks - ProgramTime.Ticks)*202);
            }
        }    
    }
     
}
