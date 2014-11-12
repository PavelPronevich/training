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
        public int Number{get;private set;}// номер порта (телефона)
        private static int _namber = 754318;
        
        //private int pinCode; // секр номер для телефона, чтобы он прослушивал Port
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


        public void PortCallToStation(object sender, TerminalCallToEventArgs e)
        {
            if (this.IsSwitched && !this.IsBusy)
            {
                this.IsBusy = true;
                PortCallToStationEventArgs args = new PortCallToStationEventArgs();
                args.PhoneNumber = e.PhoneNumber;
                OnPortCallToStation(args);
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
        
        

    }
    
}
