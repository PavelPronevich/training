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
        public Port SimCard { get; private set;}
        public TariffPlan Plan { get; private set;}
        public bool IsSpeaking { get; private set; }
        public Subscriber(Human person, Terminal phone, Port port, TariffPlan plan)
        {
            this.Person = person;
            this.Phone = phone;
            this.SimCard = port;
            this.Plan = plan;
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
                //Console.WriteLine("Абонент {0}({1}) ответил на звонок от {2}", this.Person.Surname + " " + this.Person.Name, this.SimCard.Number, e.PhoneNumber);
                this.IsSpeaking = true;
                this.Phone.ConfirmCall(true,false,true,false);
            }
            else this.Phone.ConfirmCall(false,true,true,false);

        }
        public void DisconnectSubscriber(object sender, DisconnectionEventArgs e)
        {
            this.IsSpeaking = false;
            if (e.Messege != "") Console.WriteLine(e.Messege);
        }

        public void FinishCall()
        {
            this.IsSpeaking = false;
            this.Phone.FinishCall();
        }
    }
}
