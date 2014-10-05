using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace zadacha
{
    class Program
    {
        static void Main(string[] args)
        {
            carrot  ingr1 = new carrot();
            ingr1.massa = 200;
            
            Console.WriteLine("В {0} граммов {1} содержится {2} грамма протеина", ingr1.massa,ingr1.getName(),ingr1.getProteins() );
            Console.ReadKey();
            Console.WriteLine(ingr1.getVegetableType());
            Console.ReadKey();
            //
 
            
        }
    }
}
