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
            carrot  ingr1 = new carrot();
            ingr1.massa = 0;
                                
            
            //тестирование 
            Console.WriteLine("{0}, {1}, {2}, б {3}, ж {4}, у{5}, к{6}, общ кал {7}", ingr1.getVegetableType(), ingr1.getName(), 
                ingr1.getIso(), ingr1.getProteins(), ingr1.getFats(), ingr1.getCarbohydrates(), ingr1.getCalories(), ingr1.getEnergeticValue());
            Console.ReadKey();
            radish ingr2 = new radish();
            ingr2.massa = 200;
            Console.WriteLine("{0}, {1}, {2}, б {3}, ж {4}, у{5}, к{6}, общ кал {7}", ingr2.getVegetableType(), ingr2.getName(),
                ingr2.getIso(), ingr2.getProteins(), ingr2.getFats(), ingr2.getCarbohydrates(), ingr2.getCalories(), ingr1.getEnergeticValue());

            Console.ReadKey();
           
            
        }
    }
}
