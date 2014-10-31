using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextToSEntence
{
    class WordComparer: IEqualityComparer<Word>
    {

        public bool Equals(Word x, Word y)
        {
            bool Bool;
            //if (x.ToString().ToLower() == y.ToString().ToLower())
            if (x.ToString() == y.ToString()) 
            {
                Bool=true;
            }
            else
            {
                Bool = false;
            }

            return Bool;
        }

        public int GetHashCode(Word obj)
        {
            
            return ((obj.Value).ToString()).GetHashCode();
        }
    }
}
