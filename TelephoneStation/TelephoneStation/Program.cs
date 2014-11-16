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
            Human human1 = new Human("Nikolia", "", "Broun", 1985, 120);
            Human human2 = new Human("Mikl", "", "Grin", 1984, 320);
            Human human3 = new Human("Rebeka", "", "Rebit", 1990, 122);
            Human human4 = new Human("Monika", "", "Kurva", 1989, 132);
            Human human5 = new Human("Peppa", "", "Pig", 2005, 132);
            Human human6 = new Human("Djordj", "", "Pig", 2007, 100);
            Human human7 = new Human("Danny", "", "Dog", 2006, 132);
            Human human8 = new Human("Pedro", "", "Pony", 2005, 300);
            Human human9 = new Human("Emily", "", "Elephant", 2000, 20);
            TariffPlan plan = new TariffPlan();
            List<Subscriber> Subscribers=new List<Subscriber>();
            Subscribers.Add(ATS.ConcludeContract(human1, plan));
            Subscribers.Add(ATS.ConcludeContract(human2, plan));
            Subscribers.Add(ATS.ConcludeContract(human3, plan));
            Subscribers.Add(ATS.ConcludeContract(human4, plan));
            Subscribers.Add(ATS.ConcludeContract(human5, plan));
            Subscribers.Add(ATS.ConcludeContract(human6, plan));
            Subscribers.Add(ATS.ConcludeContract(human7, plan));
            Subscribers.Add(ATS.ConcludeContract(human8, plan));
            Subscribers.Add(ATS.ConcludeContract(human9, plan));
            List<int> TelephoneNumbers=new List<int>();
                for (int i=754301;i<754310;i++)
                    TelephoneNumbers.Add(i);
            Random random=new Random();

            Console.WriteLine(1);
            for(int i = 0; i < 100; i++ )
            { 
                //Subscribers[random.Next(9)].FinishCall();
                System.Threading.Thread.Sleep(1);
                Subscribers[random.Next(9)].FinishCall(); 
                Subscribers[random.Next(9)].CallTo(TelephoneNumbers[random.Next(9)]);
                Subscribers[random.Next(9)].CallTo(TelephoneNumbers[random.Next(9)]);
                Subscribers[random.Next(9)].CallTo(TelephoneNumbers[random.Next(9)]);
                Subscribers[random.Next(9)].FinishCall();
            }

            
            /*
            subscriber1.CallTo(1232);
            subscriber1.CallTo(754301);
            subscriber2.CallTo(754303);
            subscriber1.FinishCall();
             
            
            subscriber2.CallTo(754318);
            subscriber4.FinishCall();
            subscriber2.CallTo(754318);
             * */

            ATS.GetAllFinishedCalls();

            
            

           
            Console.ReadKey();
            

            
        }
        
    }
    
}
