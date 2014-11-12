using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneStation
{
    class Program
    {
        static void Main(string[] args)
        {
            Station ATS = new Station();
            //ATS.BlockPort += Port.Block;
            

            Port port1=new Port();
            Port port2 = new Port();
           
            ATS.Add(port1);
            ATS.Add(port2);
            ATS.Check();
            Console.WriteLine(port1.Number);
            Console.WriteLine(port2.Number);
            Console.WriteLine(port1.IsSwitched);
            Console.WriteLine(port2.IsSwitched);
            Console.ReadKey();
        }
        
    }
    
}
