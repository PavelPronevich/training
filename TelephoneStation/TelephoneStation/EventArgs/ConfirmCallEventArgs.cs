using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneStation
{
    public class ConfirmCallEventArgs
    {
        public bool? IsConfirmCall;
        public bool? IsSubscriberBusy;
        public bool? IsTerminalSwitched;
        public bool? IsTerminalBusy;
        public Port ConfirfCallPort;
    }
}
