using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneStation
{
    public class Contract
    {
        public ITariffPlan ClientPlan { get; private set; }
        public Port ClientPort { get; private set; }
        public string ClientName { get; private set; }
        public string ClientSecondName { get; private set; }
        public string ClientSurname { get; private set; }
        public int ClientDateOfBirth { get; private set; }
        public Guid ClientID { get; private set; }
        public double SummToPay { get; set; }
        public Contract(Human human, ITariffPlan clientPlan, Port port)
        {
            this.ClientPlan = clientPlan;
            this.ClientPort = port;
            this.ClientName = human.Name;
            this.ClientSecondName = human.SecondName;
            this.ClientSurname = human.Surname;
            this.ClientDateOfBirth = human.DateOfBirth;
            this.ClientID = human.ID;
            this.SummToPay = 0;
         }

        public virtual void OnReportCalls(ReportCallsEventArgs e)
        {
            EventHandler<ReportCallsEventArgs> handler = ReportCalls;
            if (handler != null) handler(this, e);
        }
        public virtual void ChangeTarif()//?
        {
        }

        public event EventHandler<ReportCallsEventArgs> ReportCalls;
    }
}
