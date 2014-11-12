using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneStation
{
    class Terminal
    {
        public bool Call(Terminal target)
        {
            EventArgs args = null;
            OnStartingCall(this, args);
            if (args != null)
            {
                OnStartedCall(this, args);
            }
        }
        protected void OnStartingCall(object sender, EventArgs e)
        {
            var temp = StartingCall;
            if (temp != null)
            {
                StartingCall(sender, e);
            }
        }
    }
}
