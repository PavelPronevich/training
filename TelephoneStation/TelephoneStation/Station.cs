using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneStation
{
    public class Station
    {
        private static int transactionNumber = 1;
        private List<Port> _ports = new List<Port>();
        private List<Contract>subscribersContracts = new List<Contract>();
        private List<Calls> waitingCalls=new List<Calls>();
        private List<Calls> activeCalls=new List<Calls>();
        private List<Calls> finishedCalls=new List<Calls>();
        private List<List<Calls>> ArchiveOfCalls=new List<List<Calls>>();
        private DateTime dateOfStationCreation;
        private int monthEnd=0;
        private int CheckDay=5;
        private int CheckDayPay = 20;
        private bool isCheckDayWas=false;
        private bool isCheckDayPayWas = false;
        public double Cash { get; private set; }
    
        public Station() 
        {
            dateOfStationCreation=DateTime.Now;
        }


        public Subscriber ConcludeContract(Human human, ITariffPlan clientPlan)
        {
            Port _port = new Port();
            Contract _contract=new Contract(human,clientPlan,_port);
            subscribersContracts.Add(_contract);
            Terminal terminal = new Terminal();
            Subscriber _subscriber = new Subscriber(human, terminal, _port.Number, clientPlan);
            _contract.ReportCalls += _subscriber.AddReport;
            _port.PortToStation += ConnectToColl;
            _port.PortConfirmCall += ConfirmCall;
            _port.PortFinishCall += finishCall;
            _port.PortToTerminal += terminal.IncomingCall;
            _port.DisconnectionTerminal += terminal.DisconnectionSubscriber;
            terminal.TerminalConfirmCall += _port.ConfirmCall;
            terminal.TerminalCallTo += _port.PortCallToStation;
            terminal.TerminalIncomingCall += _subscriber.IncomingCall;
            terminal.SubscriberDisconnection += _subscriber.DisconnectSubscriber;
            terminal.TerminalFinishCall += _port.FinishCall;
            return _subscriber;
      }

        public void CheckPay()
        {
            foreach (Contract contract in subscribersContracts)
            {
                if (contract.SummToPay > 0)
                {
                    contract.ClientPort.IsSwitched = false;
                }
            }
        }
   

        public void ConnectToColl(object sender, PortCallToStationEventArgs e)
        {
            if (GetPort(e.PhoneNumber) != null && GetPort(e.PhoneNumber).IsSwitched && !GetPort(e.PhoneNumber).IsBusy)
            {
                Port port = GetPort(e.PhoneNumber);
                port.TransactionNumber=transactionNumber;
                e.PortColling.TransactionNumber=transactionNumber++;
                waitingCalls.Add(new Calls(transactionNumber++, e.PortColling, port));
                port.TakeCall((int)e.PortColling.Number);
            }
            else if (GetPort(e.PhoneNumber) == null)
            {
                string messege = String.Format("Station->{1}: Connection can not be established. " 
                + "Number {0} does not exist.", e.PhoneNumber, e.PortColling.Number);
                this.disconnection(e.PortColling, messege);
            }
            else if (!GetPort(e.PhoneNumber).IsSwitched)
            {
                string messege = String.Format("Station->{1}: Connection can not be established. "
                    + "Number {0} is blocked.", e.PhoneNumber, e.PortColling.Number);
                this.disconnection(e.PortColling, messege);
            }    
            else if (GetPort(e.PhoneNumber).IsBusy)
            {
                string messege = String.Format("Station->{1}: Connection can not be established. "
                    + "Subscriber's {0} port is busy.", e.PhoneNumber, e.PortColling.Number);
                this.disconnection(e.PortColling, messege);
            }
        }

        public Port GetPort(int phoneNumber)
        {
            Port port=new Port(true);
            foreach (Contract contract in subscribersContracts)
            {
                if (contract.ClientPort.Number == phoneNumber)
                {
                    port = contract.ClientPort;
                    return port;
                }
            }
            return null;
        }
        
        public Contract GetContract(Port port)
        {
            foreach (Contract contract in subscribersContracts)
            {
                if (contract.ClientPort==port) return contract;
            }
            return null;
        }

        public void GetAllAbonents()
        {
            foreach (var item in subscribersContracts)
            {
                Console.WriteLine("Surname {0}, Name: {1}, Phone Number: {2}",
                    item.ClientSurname, item.ClientName, item.ClientPort.Number);
            }
        }

        void ConfirmCall(object sender, ConfirmCallEventArgs e)
        {
            Calls _col = this.waitingCalls.Find(x => x.IncomingPort == e.ConfirfCallPort);

            if (e.IsConfirmCall == true)
            {
                _col.BeginCallTime = Time.Now;
                this.activeCalls.Add(_col);
                this.waitingCalls.Remove(_col);
                Contract contractFirst=subscribersContracts.Find(x=>x.ClientPort==_col.OutgoingPort);
                Contract contractSecond=subscribersContracts.Find(x=>x.ClientPort==_col.IncomingPort);
            }
            else if (e.IsSubscriberBusy==true)
            {
                string messege = String.Format("Station->{1}: Connection is not established. " 
                    + "Subscriber {0} is busy.",  e.ConfirfCallPort.Number , _col.OutgoingPort.Number);
                this.disconnection(_col.OutgoingPort, messege);
            }
            else if (e.IsTerminalBusy == true)
            {
                string messege = String.Format("Station->{1}: Connection is not established. "
               + "Subscriber's {0} terminal is busy.", e.ConfirfCallPort.Number, _col.OutgoingPort.Number);
                this.disconnection(_col.OutgoingPort, messege);
            }
            else if (e.IsTerminalSwitched == false)
            {
                string messege = String.Format("Station->{1}: Connection is not established. "
               + "Subscriber's {0} terminal is unswitched.", e.ConfirfCallPort.Number, _col.OutgoingPort.Number);
                this.disconnection(_col.OutgoingPort, messege);
            }
        }

        void disconnection(Port port, string messege)
        {
            port.Disconnection(messege);
        }

        void finishCall(object sender, FinishCallEventArgs e)
        {
            Calls call = this.activeCalls.Find(x => ((x.IncomingPort == e.PoartFinishedCall) 
                || (x.OutgoingPort == e.PoartFinishedCall)));
            if (!(call==null))
            {
                call.EndCallTime = Time.Now;
                this.activeCalls.Remove(call);
                this.finishedCalls.Add(call);
                if (call.IncomingPort==e.PoartFinishedCall)
                {
                    this.disconnection(call.OutgoingPort, "");
                }
                else this.disconnection(call.IncomingPort, "");
            }

            if (!isCheckDayWas && !(dateOfStationCreation.AddMonths(monthEnd).Month==Time.Now.Month) 
                && Time.Now.Day>this.CheckDay)
            {
                var q = from item in finishedCalls
                        where item.BeginCallTime.Month == dateOfStationCreation.AddMonths(monthEnd).Month
                        select item;
                List<Calls> colls=new List<Calls>();
                colls.AddRange(q);
                ArchiveOfCalls.Add(colls);
                finishedCalls.RemoveAll(x=> x.BeginCallTime.Month==dateOfStationCreation.AddMonths(monthEnd).Month);
                sendPaymentOrders(colls);
                isCheckDayWas=true;
                isCheckDayPayWas = false;
                
            }
            if (isCheckDayWas && !isCheckDayPayWas && Time.Now.Day > this.CheckDayPay)
            {
                CheckPay();
                isCheckDayPayWas = true;
                isCheckDayWas = false;
                monthEnd++;
            }
            
        }

        void sendPaymentOrders(List<Calls> colls)
        { 
            foreach(var contract in subscribersContracts)
            {
                var q=from item in colls
                      where item.OutgoingPort == contract.ClientPort
                      select item;
                List<Calls> collsToPay=new List<Calls>();
                collsToPay.AddRange(q);
                contract.SummToPay = contract.ClientPlan.GetDebt(collsToPay);
                List<SubscriberCall> reportCalls=new List<SubscriberCall>(); 
                
                foreach(var item in  collsToPay)
                {
                    SubscriberCall subscrCall = new SubscriberCall();
                    subscrCall.Abonent = GetContract(item.IncomingPort).ClientSurname + GetContract(item.IncomingPort).ClientName;
                    subscrCall.IncomingPhoneNumber = item.IncomingPort.Number;
                    subscrCall.BeginCallTime = item.BeginCallTime;
                    subscrCall.EndCallTime = item.EndCallTime;
                    subscrCall.Duration = contract.ClientPlan.GetNumberOfMinutes(item);
                    subscrCall.Price = contract.ClientPlan.GetDebt(item);
                    reportCalls.Add(subscrCall);

                }

                ReportCallsEventArgs args = new ReportCallsEventArgs();
                args.RepottCalls=reportCalls;
                args.SummToPay = contract.ClientPlan.GetDebt(collsToPay);
                contract.OnReportCalls(args);
            }
        }

        public void GetAllFinishedCalls()
        {
            foreach (var item in this.finishedCalls)
            {
                Console.WriteLine(item.OutgoingPort.Number + "->" + item.IncomingPort.Number +": " 
                    +item.BeginCallTime+" - "+ item.EndCallTime);
            }
        }

        public void TakePay(Subscriber subscriber)
        {
            
        }

    }
    
}

