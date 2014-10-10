using System;
using System.Collections;
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
            
            Salad<FoodStuff> salat = new Salad<FoodStuff>();
            salat.Name = "All Inclusive";
            salat.Add(new Tomatoes(230));
            salat.Add(new ChineseCabbage(140));
            salat.Add(new Lettuce(24));
            salat.Add(new Radish(50));
            salat.Add(new Onions(70));
            salat.Add(new Carrot(130));
            salat.Add(new Beets(130));


            Console.WriteLine("Salad {0}", salat.GetName());
            
            foreach(FoodStuff ingr in salat )
            {
                Console.WriteLine("{0}, {1}", ingr.GetName(), ingr.GetCaloriesNorm());
            }
            Console.ReadKey();

            Console.WriteLine(salat.GetCalories());
            Console.ReadKey();

            salat.Sort();
            foreach (FoodStuff ingr in salat)
            {
                Console.WriteLine(ingr.GetCaloriesNorm() );
            }
            Console.ReadKey();
            Console.WriteLine("dsdsdfs");

            IList<FoodStuff> salat3 = salat.RangeOfValues(13, 22);
            foreach (FoodStuff ingr in salat3)
            {
                Console.WriteLine("{0}, {1}", ingr.GetName(), ingr.GetCaloriesNorm());
            }
            Console.ReadKey();

            


        }
    }
}
