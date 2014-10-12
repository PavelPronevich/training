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
            
            Salad<FoodStuff> salad = new Salad<FoodStuff>();
            salad.Name = "All Inclusive";
            salad.Add(new Tomatoes(230));
            salad.Add(new ChineseCabbage(140));
            salad.Add(new Lettuce(24));
            salad.Add(new Radish(50));
            salad.Add(new Onions(70));
            salad.Add(new Carrot(130));
            salad.Add(new Beets(130));


            Console.WriteLine("Salad \"{0}\"", salad.GetName());
            
            foreach(FoodStuff ingr in salad )
            {
                Console.WriteLine("{0}, {1}", ingr.GetName(), ingr.GetCaloriesNorm());
            }
            Console.WriteLine();

            Console.WriteLine("Salad \"{0}\"  contains {1:f2} kilocalories in 100 gramms.", salad.GetName(), salad.GetCaloriesNorm());
            Console.WriteLine("\n");



            salad.Sort();
            
            Console.WriteLine("Sort salad \"{0}\" ingredients by the number of calories in 100 gramms:", salad.GetName());
           
            foreach (FoodStuff ingr in salad)
            {
                Console.WriteLine("{0}, {1}", ingr.GetName(), ingr.GetCaloriesNorm());
            }
            Console.WriteLine("\n");


            //Console.ReadKey();
            //Console.WriteLine("\n"); 
            //IList<FoodStuff> salat3 = salad.RangeOfValuesToList(15, 35);
            //foreach (FoodStuff ingr in salat3)
            //{
            //    Console.WriteLine("{0}, {1}", ingr.GetName(), ingr.GetCaloriesNorm());
            //}
            //Console.ReadKey();
            //Console.WriteLine("\n"); 
            
            salad.RangeOfValuesToType(15, 35);
            Console.ReadKey();
            
        }
    }
}
