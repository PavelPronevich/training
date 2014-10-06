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
            // нулевая масса 
          //  Carrot ingr1 = new Carrot(100);
           
           // BaseClass[] salad=new BaseClass[3];
            //salad[0]=new Tomatoes(230);
            //salad[1]=new ChineseCabbage(140);
            //salad[2]=new Lettuce(24);

            //Console.WriteLine(salad[0].CompareTo(salad[2]));

            //foreach(BaseClass ingr in salad)
            //{
            //    Console.WriteLine("{0}, {1}", ingr.getName(), ingr.getProteins());
            //}


            
            ArrayList salat1 = new ArrayList();
            salat1.Add(new Tomatoes(230));
            salat1.Add(new ChineseCabbage(140));
            salat1.Add(new Lettuce(24));

            Console.WriteLine("");
            Console.WriteLine("");

            double EV = 0;
            for (int i = 0; i < salat1.Count; i++ )
            {
                EV += (salat1[i] as BaseClass).getEnergeticValue();
                Console.WriteLine("{0}, {1}, {2}",(salat1[i] as BaseClass).getName(), (salat1[i]as BaseClass).getProteins(),
                    (salat1[i] as BaseClass).getEnergeticValue());
            }
            Console.WriteLine("В салате содержится {0} ккал.", EV);

            Console.WriteLine("");
            Console.WriteLine("");
            Console.ReadKey();



            // сортировка

            salat1.Sort();
            

            for (int i = 0; i < salat1.Count; i++)
            {
                Console.WriteLine("{0}, {1}, {2}", (salat1[i] as BaseClass).getName(), (salat1[i] as BaseClass).getProteins(),
                    (salat1[i] as BaseClass).getEnergeticValue());
            }
            Console.ReadKey();


                //тестирование 
                //Console.WriteLine("{0}, {1}, {2}, б {3}, ж {4}, у{5}, к{6}, общ кал {7}", ingr1.getVegetableType(), ingr1.getName(),
                //   ingr1.getIso(), ingr1.getProteins(), ingr1.getFats(), ingr1.getCarbohydrates(), ingr1.getCalories(), ingr1.getEnergeticValue());
            
             
            
        }
    }
}
