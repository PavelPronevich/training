using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_2_New
{
    class WordNew
    {
        public List<Letter> Value;
        public WordNew(List<Letter> word)
        {
            this.Value=word; 
        }

        public override string ToString()
        {
            StringBuilder wordString=new StringBuilder();
            foreach (Letter item in Value)
            {
                wordString.Append(item.ToString());
            }
            return wordString.ToString();
        }
    }
}
