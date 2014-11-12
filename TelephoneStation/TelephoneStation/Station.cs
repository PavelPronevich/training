using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneStation
{
    class Station
    {
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
    Contract _contract=new Contract(human,clientPlan,_port);
    subscribers.Add(_contract);
    return new Subscriber(human, new Terminal(), _port, clientPlan);
       // подумать нужен ли пользователю порт или просто номер порта
   }
        public void Check()
    {
        foreach (Contract contract in subscribers)
        {
            if (true)//проверка на оплату
            {
                if (contract.ClientPort.Isswitched)
                {
                    
                    /*BlockPortEventArgs args = new BlockPortEventArgs();
                    args.port = port;
                    args.IsSwitched = false;
                    OnBlockPort(args);*/
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
     public   event EventHandler<BlockPortEventArgs> BlockPort;
        
        
    }
    
}
