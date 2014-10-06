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
            // нулевая масса 
            Carrot ingr1 = new Carrot(100);
                                
            
            //тестирование 
            Console.WriteLine("{0}, {1}, {2}, б {3}, ж {4}, у{5}, к{6}, общ кал {7}", ingr1.getVegetableType(), ingr1.getVegetableName(),
                ingr1.getVegetableIso(), ingr1.getProteins(), ingr1.getFats(), ingr1.getCarbohydrates(), ingr1.getCalories(), ingr1.getEnergeticValue());
            Console.ReadKey();
            
        }
    }
}
