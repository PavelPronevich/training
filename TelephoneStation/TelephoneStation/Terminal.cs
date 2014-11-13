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
    }
}
