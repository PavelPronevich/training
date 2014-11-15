using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneStation
{
    class Calls
    {
        public int TransactionNumber { get; set; }
        public Port OutgoingPort { get; set; }
        public Port IncomingPort { get; set; }
        public DateTime BeginCallTime { get; set; }
        public DateTime EndCallTime { get; set; }
        public Calls()
        {
        }
        public Calls(int transactionNumber, Port outgoingPort, Port incomingPort)
        {
            this.IncomingPort = incomingPort;
            this.OutgoingPort = outgoingPort;
            this.TransactionNumber = transactionNumber;
        }
    }
}
