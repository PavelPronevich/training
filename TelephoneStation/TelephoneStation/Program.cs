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
            Station TelephoneStation = new Station();
            Human human1 = new Human("Nikolia", "", "Broun", 1985, 120);
            Human human2 = new Human("Mikl", "", "Grin", 1984, 320);
            Human human3 = new Human("Rebeka", "", "Rebit", 1990, 122);
            Human human4 = new Human("Monika", "", "Kurva", 1989, 132);
            Human human5 = new Human("Peppa", "", "Pig", 2005, 132);
            Human human6 = new Human("Djordj", "", "Pig", 2007, 100);
            Human human7 = new Human("Danny", "", "Dog", 2006, 132);
            Human human8 = new Human("Pedro", "", "Pony", 2005, 300);
            Human human9 = new Human("Emily", "", "Elephant", 2000, 20);
            
            
            List<Subscriber> Subscribers=new List<Subscriber>();
            Subscribers.Add(TelephoneStation.ConcludeContract(human1, new TariffLight(754308)));
            Subscribers.Add(TelephoneStation.ConcludeContract(human2, new TariffLight(754307)));
            Subscribers.Add(TelephoneStation.ConcludeContract(human2, new TariffBase())); ;
            Subscribers.Add(TelephoneStation.ConcludeContract(human2, new TariffLight(754301))); ;
            Subscribers.Add(TelephoneStation.ConcludeContract(human2, new TariffBase())); ;
            Subscribers.Add(TelephoneStation.ConcludeContract(human2, new TariffLight(754303))); ;
            Subscribers.Add(TelephoneStation.ConcludeContract(human2, new TariffLight(754304))); ;
            Subscribers.Add(TelephoneStation.ConcludeContract(human2, new TariffLight(754305))); ;
            Subscribers.Add(TelephoneStation.ConcludeContract(human9, new TariffBase()));
            List<int> TelephoneNumbers=new List<int>();
                for (int i=754301;i<754310;i++)
                    TelephoneNumbers.Add(i);
            Random random=new Random();

                        
            for(int i = 0; i < 700; i++ )
            { 
                System.Threading.Thread.Sleep(1);
                Subscribers[random.Next(9)].FinishCall(); 
                Subscribers[random.Next(9)].CallTo(TelephoneNumbers[random.Next(9)]);
                Subscribers[random.Next(9)].CallTo(TelephoneNumbers[random.Next(9)]);
                System.Threading.Thread.Sleep(1);
                Subscribers[random.Next(9)].FinishCall(); 
                Subscribers[random.Next(9)].CallTo(TelephoneNumbers[random.Next(9)]);
                Subscribers[random.Next(9)].FinishCall();
            }


            Console.WriteLine("{0} {1} made in the reporting month following calls", Subscribers[1].Person.Surname,
                Subscribers[1].Person.Name);
            Subscribers[1].GetAllReportCalls();
            Console.WriteLine("Sum to pay, taking into account the tariff {0}, is {1}$",
                Subscribers[1].Plan.Name,Subscribers[1].SummToPay);
            Console.ReadKey();
            
        }
        
    }
    
}
