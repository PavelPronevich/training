﻿using System;
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
            ATS.Add(port1);
            ATS.Check();
            Console.WriteLine(port1.Number);
            Console.WriteLine(port1.Isswitched);
            Console.ReadKey();
        }
        
    }
    
}
