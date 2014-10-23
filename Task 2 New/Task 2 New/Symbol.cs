using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_2_New
{
    class Symbol
    {
        public char Value;
        public bool isQuestion { get; private set; }
        public Symbol(char value)
        {
            this.Value = value;
            if (value.Equals('?'))
            {
                isQuestion=true;
            }
            else
            {
                isQuestion=false;
            }
        }
        override public string ToString()
        {
            return this.Value.ToString();
        }
    }
}