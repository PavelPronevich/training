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
    public Station() 
    {
        BlockPort += Port.Block;
    }

   public void Add(Port port)
   {
       _ports.Add(port);
   }
        public void Check()
    {
        foreach (Port port in _ports)
        {
            if (true)//проверка на оплату
            {
                if (port.Isswitched)
                {
                    BlockPortEventArgs args = new BlockPortEventArgs();
                    args.port = port;
                    args.IsSwitched = false;
                    OnBlockPort(args);
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
        event EventHandler<BlockPortEventArgs> BlockPort;
        
        
    }
    
}
