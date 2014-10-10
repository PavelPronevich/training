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
            
            // создани салата
            ArrayList salat1 = new ArrayList();
            salat1.Add(new Tomatoes(230));
            salat1.Add(new ChineseCabbage(140));
            salat1.Add(new Lettuce(24));
            salat1.Add(new Onions(70));

            double minEV = 20;
            double maxEV = 80;
            double EV = 0;

            for (int i = 0; i < salat1.Count; i++ )
            {
                EV += (salat1[i] as FoodStuff).GetCalories();
                Console.WriteLine("В ингридиенте {0} массы {1} содржится {2} Ккал", (salat1[i] as FoodStuff).GetName(), 
                    (salat1[i] as FoodStuff).GetMassa(), (salat1[i] as FoodStuff).GetCalories());

            }
            Console.WriteLine("В салате содержится {0} ккал.", EV);

            Console.WriteLine();
            Console.ReadKey();


            // выборка продуктов с заданным знач колорийности 
            for (int i = 0; i < salat1.Count; i++)
            {
                if (((salat1[i] as FoodStuff).GetCalories() >= minEV) &&
                    ((salat1[i] as FoodStuff).GetCalories() <= maxEV)) {
                        Console.WriteLine("В ингридиенте {0} содержится {1} Ккал", (salat1[i] as FoodStuff).GetName(), (salat1[i] as FoodStuff).GetCalories());
                }
            }

            Console.WriteLine("");
            Console.WriteLine("");
            Console.ReadKey();



            // сортировка по содржанию белка в 100гр продукта

            salat1.Sort();
            

            for (int i = 0; i < salat1.Count; i++)
            {
                Console.WriteLine("В ингридиенте {0} содержится {1} гр белка на 100 гр", 
                    (salat1[i] as FoodStuff).GetName(), (salat1[i] as FoodStuff).GetProteinsNorm());
            }
            Console.ReadKey();


                //тестирование 
                //Console.WriteLine("{0}, {1}, {2}, б {3}, ж {4}, у{5}, к{6}, общ кал {7}", ingr1.getVegetableType(), ingr1.getName(),
                //ingr1.getIso(), ingr1.getProteins(), ingr1.getFats(), ingr1.getCarbohydrates(), ingr1.getCalories(), ingr1.getEnergeticValue());

            Console.WriteLine();
            Console.WriteLine();
            Salad<FoodStuff> salat2 = new Salad<FoodStuff>();
            salat2.Name = "All Inclusive";
            salat2.Add(new Tomatoes(230));
            salat2.Add(new ChineseCabbage(140));
            salat2.Add(new Lettuce(24));
            salat2.Add(new Onions(70));

            for (int i = 0; i < salat1.Count; i++)
                {
                    EV += (salat1[i] as FoodStuff).GetCalories();
                    Console.WriteLine("В ингридиенте {0} массы {1} содржится {2} Ккал", (salat1[i] as FoodStuff).GetName(), 
                        (salat1[i] as FoodStuff).GetMassa(), (salat1[i] as FoodStuff).GetCalories());
                }

            foreach(FoodStuff ingr in salat2 )
            {
                Console.WriteLine(ingr.GetCalories());
            }
            Console.ReadKey();

            Console.WriteLine(salat2.GetCalories());
            Console.ReadKey();

            salat2.Sort();
            foreach (FoodStuff ingr in salat2)
            {
                Console.WriteLine(ingr.GetProteinsNorm() );
            }
            Console.ReadKey();
        }
    }
}
