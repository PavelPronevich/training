using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneStation
{
    public class Subscriber
    {
        public Human Person { get; private set;}
        public Terminal Phone { get; private set;}
        public double SummToPay { get; private set; }
        public int PhoneNumber{ get; private set;}
        public ITariffPlan Plan { get; private set; }
        public bool IsSpeaking { get; private set; }
        public List<SubscriberCall> ReportCalls { get; private set; }
        
        public Subscriber(Human person, Terminal phone, int phoneNumber, ITariffPlan plan)
        {
            this.Person = person;
            this.Phone = phone;
            this.PhoneNumber =phoneNumber;
            this.Plan = plan;
            this.SummToPay = 0;
        }
        
        public void CallTo(int phoneNumber)
        {
            if (!Phone.IsSwitched) Phone.IsSwitched = true;
            if (!Phone.IsBusy)
            {
                Phone.IsBusy = true;
                this.IsSpeaking = true;
                this.Phone.CallTo(phoneNumber);
            }
        }
        
        public void IncomingCall(object sender, TerminalIncomingCallToSubscriberEventArgs e)
        {
            if (!this.Person.IsBusy)
            {
                this.IsSpeaking = true;
                this.Phone.ConfirmCall(true,false,true,false);
            }
            else this.Phone.ConfirmCall(false,true,true,false);
        }
        
        public void DisconnectSubscriber(object sender, DisconnectionEventArgs e)
        {
            this.IsSpeaking = false;
        }

        public void FinishCall()
        {
            this.IsSpeaking = false;
            this.Phone.FinishCall();
        }

        public void AddReport(object sender, ReportCallsEventArgs e)
        {
            SummToPay = e.SummToPay;
            ReportCalls=e.RepottCalls;
        }

        public void GetAllReportCalls()
        {
            foreach (var item in ReportCalls)
            {
                Console.WriteLine("Subscriber: {0}; Duration: {1}; Price: {2}.", item.IncomingPhoneNumber, 
                    item.Duration,item.Price);
            }
        
        }
    }
}
