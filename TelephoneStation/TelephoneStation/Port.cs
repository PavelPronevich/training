using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneStation
{
    public class Port
    {
        private bool _isSwitched=true;
        private bool _isBusy=false;
        public int Number { get; private set;}
        public int TransactionNumber { get; set; }
        private static int _namber = 754301;
        public Port(bool a)
        { this.Number = 0; }
        
        public Port() 
        {
            this.Number = _namber++;
        }
        
        public bool IsSwitched 
        {
            get { return _isSwitched; }
            set { _isSwitched=value;  }
        }
        
        public bool IsBusy
        {
            get { return _isBusy;}
            private set { _isBusy = value;}
        }

        public void TakeCall(int phoneNumber)
        {
            _isBusy = true;
            PortCallToTerminalEventArgs args = new PortCallToTerminalEventArgs();
            args.PhoneNumber = phoneNumber;
            OnPortCallToTerminal(args);
        }
        
        protected virtual void OnPortCallToTerminal(PortCallToTerminalEventArgs e)
        {
            EventHandler<PortCallToTerminalEventArgs> handler = PortToTerminal;
            if (handler != null) handler(this, e);
        }
                
        public void PortCallToStation(object sender, TerminalCallToEventArgs e)
        {
            if (this.IsSwitched && !this.IsBusy)
            {
                this.IsBusy = true;
                PortCallToStationEventArgs args = new PortCallToStationEventArgs();
                args.PhoneNumber = e.PhoneNumber;
                args.PortColling = this;
                OnPortCallToStation(args);
             }
            else { /*невозможно сделать звонок, сообщить терминалу*/}
            









        }
        protected virtual void OnPortCallToStation(PortCallToStationEventArgs e)
        {
            EventHandler<PortCallToStationEventArgs> handler = PortToStation;
            if (handler != null) handler(this, e);
        }
        
        public void ConfirmCall(object sender, ConfirmCallEventArgs e)
        {
            if (!e.IsConfirmCall == true) this._isBusy = false;

            e.ConfirfCallPort = this;
            EventHandler<ConfirmCallEventArgs> handler = PortConfirmCall;
            if (handler != null) handler(this, e);
        }

        public void Disconnection(string messege)
        {
            this.IsBusy = false;
            DisconnectionEventArgs args = new DisconnectionEventArgs();
            args.Messege=messege;
            OnDisconnectionTerminal(args);
        }

        protected virtual void OnDisconnectionTerminal(DisconnectionEventArgs e)
        {
            EventHandler<DisconnectionEventArgs> handler = DisconnectionTerminal;
            if (handler != null) handler(this, e);
        }

        public void FinishCall(object sender, FinishCallEventArgs e)
        {
            this._isBusy = false;
            e.PoartFinishedCall = this;
            OnFinishCall(e);
        }

        protected virtual void OnFinishCall(FinishCallEventArgs e)
        {
            EventHandler<FinishCallEventArgs> handler = PortFinishCall;
            if (handler != null) handler(this, e);
        }
     
        public event EventHandler<FinishCallEventArgs> PortFinishCall;
        public event EventHandler<PortCallToTerminalEventArgs> PortToTerminal;
        public event EventHandler<DisconnectionEventArgs> DisconnectionTerminal;
        public event EventHandler<ConfirmCallEventArgs> PortConfirmCall;
        public event EventHandler<PortCallToStationEventArgs> PortToStation;
    }
}
