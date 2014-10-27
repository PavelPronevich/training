using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2_25_10
{
    class CombinationSymbol: ISentencePart
    {
        //нужна ли функция getLength()? подумать
        public List<Symbol> Value;
        public CombinationSymbol(List<Symbol> combinationSymbol)
        {
            this.Value=combinationSymbol; 
        }

        override public string ToString()
        {
            StringBuilder wordString=new StringBuilder();
            foreach (Symbol item in Value)
            {
                wordString.Append(item.ToString());
            }
            return wordString.ToString();
        }
        public int getLength()
        {
            return Value.Count;
        }

        public Symbol getLastSymbol()
        {
            return Value[Value.Count-1];
        }
    }
}
