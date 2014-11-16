using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneStation
{
    
    public class Terminal
    {
        public bool IsSwitched { get; set; }
        public bool IsBusy { get; set; }
        public void CallTo(int phoneNumber)
        {
            TerminalCallToEventArgs args = new TerminalCallToEventArgs();
            args.PhoneNumber = phoneNumber;
            OnTerminalCallTo(args);
            //Console.WriteLine("Терминал отправляет звонок на номер {0} порту", phoneNumber);

            //отпр сообщ порту и перевод его в состояние занят
            //прием сообщение от порта об успехе/неуспехе передачи сообщения станции

            //сформировать и передать событие терминалу по переводу терминала в состоянии занято и передаче номера абонента
            //возможно создать уникальный  номер для отслеживания состояния операции
        }
        protected virtual void OnTerminalCallTo(TerminalCallToEventArgs e)
        {
            EventHandler<TerminalCallToEventArgs> handler = TerminalCallTo;
            if (handler != null) handler(this, e);
        }
        public Terminal()
        {
            this.IsSwitched = true;
            this.IsBusy = false;
        }


        public event EventHandler<TerminalCallToEventArgs> TerminalCallTo;

        
        public void IncomingCall(object sender, PortCallToTerminalEventArgs e)
        {
            if (this.IsSwitched && !this.IsBusy)
            {
                this.IsBusy = true;
                TerminalIncomingCallToSubscriberEventArgs args = new TerminalIncomingCallToSubscriberEventArgs();
                args.PhoneNumber = e.PhoneNumber;
                OnTerminalIncomingCallToSubscriber(args);
                //Console.WriteLine("Входящий звонок от {0}", e.PhoneNumber);

            }
            else if (!this.IsSwitched)
            {
                ConfirmCall(null, null, false, null);
            }
            else if (this.IsBusy)
            {
                
                ConfirmCall(null, null, true, true);
                //{ Console.WriteLine("Невозможно дозвониться телефон абонента выключен или занят {0}", e.PhoneNumber); }
            }
        }
        protected virtual void OnTerminalIncomingCallToSubscriber(TerminalIncomingCallToSubscriberEventArgs e)
        {
            EventHandler<TerminalIncomingCallToSubscriberEventArgs> handler = TerminalIncomingCall;
            if (handler != null) handler(this, e);
        }

        public event EventHandler<TerminalIncomingCallToSubscriberEventArgs> TerminalIncomingCall;

        public void ConfirmCall(bool? isConfirmCall, bool? isSubscriberBusy, bool? isTerminalSwitched, bool? isTerminalBusy)
        {
            
            ConfirmCallEventArgs args = new ConfirmCallEventArgs();
            args.IsConfirmCall=isConfirmCall;
            args.IsSubscriberBusy = isSubscriberBusy;
            args.IsTerminalSwitched = isTerminalSwitched;
            args.IsTerminalBusy = isTerminalBusy;
            if (!isConfirmCall == true) this.IsBusy = false;
            OnTerminalConfirmCall(args);
        }

        protected virtual void OnTerminalConfirmCall(ConfirmCallEventArgs e)
        {
            EventHandler<ConfirmCallEventArgs> handler = TerminalConfirmCall;
            if (handler != null) handler(this, e);
        }
        public event EventHandler<ConfirmCallEventArgs> TerminalConfirmCall;

        public void DisconnectionSubscriber(object sender, DisconnectionEventArgs e)
        {
            this.IsBusy=false;
            OnDisconnectionSubscriber(e);

        }

        protected virtual void OnDisconnectionSubscriber(DisconnectionEventArgs e)
        {
            EventHandler<DisconnectionEventArgs> handler = SubscriberDisconnection;
            if (handler != null) handler(this, e);
        }
        public event EventHandler<DisconnectionEventArgs> SubscriberDisconnection;

        public void FinishCall()
        {
            FinishCallEventArgs args = new FinishCallEventArgs();
            args.IsCallFinished = true;
            this.IsBusy = false;
            OnTerminalFinishCall(args);
        }
        protected virtual void OnTerminalFinishCall(FinishCallEventArgs e)
        {
            EventHandler<FinishCallEventArgs> handler = TerminalFinishCall;
            if (handler != null) handler(this, e);
        }
        public event EventHandler<FinishCallEventArgs> TerminalFinishCall;

    }
}
