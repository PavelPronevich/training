using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneStation
{
    public class Port
    {
        private bool _isSwitched=true; // вкл.откл станция за неуплату
        private bool _isBusy=false;    // занет не занет опред станция если идет входящий или телефон исзодящим звонком
        public int? Number{get;private set;}// номер порта (телефона)
        public int TransactionNumber { get; set; }
        private static int _namber = 754318;
       
        //private int pinCode; // секр номер для телефона, чтобы он прослушивал Port
        public Port(bool a)
        {
        }

        public Port() 
        {
            this.Number = _namber++;
        }
        public bool IsSwitched 
        {
            get 
            { 
                return _isSwitched; 
            }
            set
            { 
                _isSwitched=value;
            }
         }
        public bool IsBusy
        {
            get
            {
                return _isBusy;
            }
            private set
            {
                _isBusy = value;
            }
        }

        public void TakeCall(int phoneNumber)//о вход вызове
        {
            _isBusy = true;
            PortCallToTerminalEventArgs args = new PortCallToTerminalEventArgs();
            args.PhoneNumber = phoneNumber;
            OnPortCallToTerminal(args);
            //Console.WriteLine("Входящий вызов от {0}", phoneNumber);
        }
        protected virtual void OnPortCallToTerminal(PortCallToTerminalEventArgs e)
        {
            EventHandler<PortCallToTerminalEventArgs> handler = PortToTerminal;
            if (handler != null) handler(this, e);
        }
        public event EventHandler<PortCallToTerminalEventArgs> PortToTerminal;


        public void PortCallToStation(object sender, TerminalCallToEventArgs e)
        {
            if (this.IsSwitched && !this.IsBusy)
            {
                this.IsBusy = true;
                PortCallToStationEventArgs args = new PortCallToStationEventArgs();
                args.PhoneNumber = e.PhoneNumber;
                args.PortColling = this;
                OnPortCallToStation(args);
                //Console.WriteLine("Порт принял звонок от терминала и отправляет запрос на звонок {0} к станции", e.PhoneNumber);
            }
            else { /*невозможно сделать звонок, сообщить терминалу*/}
            

        }
        protected virtual void OnPortCallToStation(PortCallToStationEventArgs e)
        {
            EventHandler<PortCallToStationEventArgs> handler = PortToStation;
            if (handler != null) handler(this, e);
        }
        public event EventHandler<PortCallToStationEventArgs> PortToStation;
       
        public static void Block(object sender, BlockPortEventArgs e)
        {
         e.port.IsSwitched = e.IsSwitched;
        }
       
        public void ConfirmCall(object sender, ConfirmCallEventArgs e)
        {
            e.ConfirfCallPort = this;
            EventHandler<ConfirmCallEventArgs> handler = PortConfirmCall;
            if (handler != null) handler(this, e);
        }
        public event EventHandler<ConfirmCallEventArgs> PortConfirmCall;

        public void Disconnection(string messege)
        {
            this.IsBusy = false;
            DisconnectionEventArgs args = new DisconnectionEventArgs();
            args.Messege=messege;
            OnDisconnectionTerminal(args);
            //Console.WriteLine("Входящий вызов от {0}", phoneNumber);
        }
        protected virtual void OnDisconnectionTerminal(DisconnectionEventArgs e)
        {
            EventHandler<DisconnectionEventArgs> handler = DisconnectionTerminal;
            if (handler != null) handler(this, e);
        }
        public event EventHandler<DisconnectionEventArgs> DisconnectionTerminal;

    }
    
}
