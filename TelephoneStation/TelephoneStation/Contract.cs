using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneStation
{
    class Contract
    {
        public TariffPlan ClientPlan { get; private set;}
        public Port ClientPort { get; private set; }
        public string ClientName { get; private set; }
        public string ClientSecondName { get; private set; }
        public string ClientSurname { get; private set; }
        public int ClientDateOfBirth { get; private set; }
        public Guid ClientID { get; private set; }
        public Contract(Human human, TariffPlan clientPlan, Port port)
        {
            this.ClientPlan = clientPlan;
            this.ClientPort = port;
            this.ClientName = human.Name;
            this.ClientSecondName = human.SecondName;
            this.ClientSurname = human.Surname;
            this.ClientDateOfBirth = human.DateOfBirth;
            this.ClientID = human.ID;
         }
        public virtual void ChangeTarif()//?
        {
        }
        public virtual void ChangePort()//?
        {
        }
        public virtual void ChangeTerminal()//?
        {
        }

            
    }
}
