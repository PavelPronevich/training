using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2_25_10
{
    class Word : ISentencePart
    {
        public List<Character> Value;
        public Word(List<Character> word)
        {
            this.Value=word; 
        }

        override public string ToString()
        {
            StringBuilder wordString=new StringBuilder();
            foreach (Character item in Value)
            {
                wordString.Append(item.ToString());
            }
            return wordString.ToString();
        }
        public int Length
        {
            get
            { return this.Value.Count; }
        }

        public Character getFirstCharacter()
        {
            return Value[0];
        }
        
        
    }
}
