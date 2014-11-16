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
    private List<Contract>subscribers = new List<Contract>();// придумать новое название
    private List<Calls> waitingCalls=new List<Calls>();
    private List<Calls> activeCalls=new List<Calls>();
    private List<Calls> finishedCalls=new List<Calls>();

    public Station() 
    {
        BlockPort += Port.Block;
        
    }

   public void Add(Port port)
   {
       _ports.Add(port);
   }

   public Subscriber ConcludeContract(Human human, TariffPlan clientPlan)
   {
    
    Port _port = new Port();

    _port.PortToStation += ConnectToColl;
    _port.PortConfirmCall += ConfirmCall;
    Contract _contract=new Contract(human,clientPlan,_port);
    subscribers.Add(_contract);
    Terminal terminal = new Terminal();
    _port.PortToTerminal += terminal.IncomingCall;
    _port.DisconnectionTerminal += terminal.DisconnectionSubscriber;

    _port.PortFinishCall += finishCall;


    terminal.TerminalConfirmCall += _port.ConfirmCall;
    

    terminal.TerminalCallTo += _port.PortCallToStation;
    Subscriber _subscriber = new Subscriber(human, terminal, _port, clientPlan);
    terminal.TerminalIncomingCall += _subscriber.IncomingCall;
    terminal.SubscriberDisconnection += _subscriber.DisconnectSubscriber;
    terminal.TerminalFinishCall += _port.FinishCall;


    return _subscriber;
       // подумать нужен ли пользователю порт или просто номер порта
   }
        public void Check()
    {
        foreach (Contract contract in subscribers)
        {
            if (true)//проверка на неоплату
            {
                if (contract.ClientPort.IsSwitched)
                {
                    contract.ClientPort.IsSwitched = false;

                    /*BlockPortEventArgs args = new BlockPortEventArgs();
                    args.port = contract.ClientPort;
                    args.IsSwitched = false;
                    OnBlockPort(args);
                     */ 
                }
            }
        }
    }
   

    protected virtual void OnBlockPort(BlockPortEventArgs e)
        {
            EventHandler<BlockPortEventArgs> handler = BlockPort;
            if (handler != null)
            {
                handler(this, e);
            }
        }
     /*public*/ event EventHandler<BlockPortEventArgs> BlockPort;
        
    public void ConnectToColl(object sender, PortCallToStationEventArgs e)
     {
        //Console.WriteLine("Станция приняла звонок от {0} порта и отправляет запрос на {1} порт",e.PortColling.Number, e.PhoneNumber);
        //Если номер не существует оборвать линию, вернуть звонившему сообщение 
        //номер не зарегистрирован, перевести все в режим доступности
         //Console.WriteLine(e.PhoneNumber);

        if (GetPort(e.PhoneNumber) != null && GetPort(e.PhoneNumber).IsSwitched && !GetPort(e.PhoneNumber).IsBusy)
        {
            Port port = GetPort(e.PhoneNumber);
            port.TransactionNumber=transactionNumber;//Может номер операции определять телефоном исход вызова
            e.PortColling.TransactionNumber=transactionNumber++;
            //Console.WriteLine("Найден порт номера {0}, устанавливается соединение", port.Number);
            waitingCalls.Add(new Calls(transactionNumber++, e.PortColling, port));
            //Console.WriteLine(waitingCalls.Count);
            //Console.ReadKey();
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
        foreach (Contract contract in subscribers)
        {
            if (contract.ClientPort.Number == phoneNumber)
            {
                port = contract.ClientPort;
                return port;
            }
        }
        return null;
    }
        

        public void GetAllAbonents()
    {
        foreach (var item in subscribers)
        {
            Console.WriteLine("Фамилия {0}, имя: {1}, номер телефона: {2}",
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
                Contract contractFirst=subscribers.Find(x=>x.ClientPort==_col.OutgoingPort);
                Contract contractSecond=subscribers.Find(x=>x.ClientPort==_col.IncomingPort);
                Console.WriteLine("Subscribers {0} {1} ({2}) and {3} {4} ({5}) are speaking", 
                    contractFirst.ClientSurname, contractFirst.ClientName,contractFirst.ClientPort.Number,
                    contractSecond.ClientSurname, contractSecond.ClientName,contractSecond.ClientPort.Number);
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
            Calls _call = this.activeCalls.Find(x => ((x.IncomingPort == e.PoartFinishedCall) 
                || (x.OutgoingPort == e.PoartFinishedCall)));
            if (!(_call==null))
            {
                _call.EndCallTime = Time.Now;
                //this.disconnection(_call.OutgoingPort, messege);
                this.activeCalls.Remove(_call);
                this.finishedCalls.Add(_call);
                if (_call.IncomingPort==e.PoartFinishedCall)
                {
                    this.disconnection(_call.OutgoingPort, "");
                }
                else this.disconnection(_call.IncomingPort, "");
            }
            //Console.WriteLine(e.PoartFinishedCall.Number + "завершил разговор");

        }

        public void GetAllFinishedCalls()
        {
            foreach (var item in this.finishedCalls)
            {
                Console.WriteLine(item.OutgoingPort.Number + "->" + item.IncomingPort.Number +": " 
                    +item.BeginCallTime+" - "+ item.EndCallTime);
            }
        }
    }
    
}

