using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneStation
{
    public class Subscriber
    {
        public Human Person{get; private set;}
        public Terminal Phone{get; private set;}
        public Port SimCard{get; private set;}
        public TariffPlan Plan{get; private set;}
        public Subscriber(Human person, Terminal phone, Port port, TariffPlan plan)
        {
            this.Person = person;
            this.Phone = phone;
            this.SimCard = port;
            this.Plan = plan;
        }
    }
}
