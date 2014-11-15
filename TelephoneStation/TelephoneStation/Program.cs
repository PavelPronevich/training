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
            TariffPlan plan = new TariffPlan();
            Subscriber subscriber1 = ATS.ConcludeContract(human1, plan);
            Subscriber subscriber2 = ATS.ConcludeContract(human2, plan);
            Subscriber subscriber3 = ATS.ConcludeContract(human3, plan);
            Subscriber subscriber4 = ATS.ConcludeContract(human4, plan);

            
            subscriber1.CallTo(1232);
            subscriber1.CallTo(754319);
            subscriber3.CallTo(754319);
            //subscriber1.CallTo(754321);
            //subscriber3.CallTo(754321);
            //subscriber4.CallTo(654323);

            ATS.GetAllAbonents();

            Console.WriteLine(DateTime.Now);
            Console.ReadKey();
            
        }
        
    }
    
}
