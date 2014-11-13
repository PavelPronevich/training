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

    Contract _contract=new Contract(human,clientPlan,_port);
    subscribers.Add(_contract);
    Terminal terminal = new Terminal();

    terminal.TerminalCallTo += _port.PortCallToStation;
    return new Subscriber(human, terminal, _port, clientPlan);
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
        Console.WriteLine("Станция приняла звонок от {0} порта и отправляет запрос на {1} порт",e.PortColling.Number, e.PhoneNumber);
        //Если номер не существует оборвать линию, вернуть звонившему сообщение 
        //номер не зарегистрирован, перевести все в режим доступности
        
        if (GetPort(e.PhoneNumber) == null)
        {
            Console.WriteLine("Номер не привязан ни к одному из абонентов", GetPort(e.PhoneNumber).Number);
        }
        else if (GetPort(e.PhoneNumber).IsBusy)
        {
            Console.WriteLine("Линия занята");
        }
        else
        {
            Port port = new Port(false);
            port = GetPort(e.PhoneNumber);
            port.TransactionNumber=transactionNumber;//Может номер операции определять телефоном исход вызова
            e.PortColling.TransactionNumber=transactionNumber;
            Console.WriteLine("Найден порт номера {0}, устанавливается соединение", port.Number);
            port.TakeCall(e.PortColling.Number);

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
            }
        }
        return port;
    }

        public void GetAllAbonents()
    {
        foreach (var item in subscribers)
        {
            Console.WriteLine("Фамилия {0}, имя: {1}, номер телефона: {2}",
              item.ClientSurname, item.ClientName, item.ClientPort.Number);
        }
    }

    }
    
}

