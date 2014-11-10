using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TelephoneStation
{
    public class Subscriber
    {
        public Human Person { get; private set; }
        public int? TerminalNumber { get; private set; }
        public bool IsBusy { get; set; }
        public Subscriber(Human person)
        {
            this.Person = person;
            this.IsBusy = false;
            //this.TerminalNumber=
        }
        

        
        public void GetTerminal()//???ConcludeContract
        {
            if (this.TerminalNumber!=null)
            {
                // ЗАпрос на АТС ФиоAge возврат номер термин 
            }
        }
        

    }
}
