using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_2_New
{
    class IntervalsNew
    {

        public List<Symbol> Value;
        public IntervalsNew(List<Symbol> symbolsBetweenWords)
        {
            this.Value = symbolsBetweenWords; 
        }

        public override string ToString()
        {
            StringBuilder wordString=new StringBuilder();
            foreach (Symbol item in Value)
            {
                wordString.Append(item.ToString());
            }
            return wordString.ToString();
        }
    }
}
