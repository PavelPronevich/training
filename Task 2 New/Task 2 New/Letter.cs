using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_2_New
{
    class Letter
    {
        public char Value;
        public bool IsConsonant { get; private set; }
        public Letter(char letter)
        {
            this.Value = letter;
            
            if (ConsonantLettersString.Contains(this.Value))
            {
                IsConsonant = true;
            }
            else
            {
                IsConsonant = false;
            }
        }

        override public string ToString()
        {
            return this.Value.ToString();
        }
        
        public string ConsonantLettersString = "QqWwRrTtPpSsDdFfGgHhJjKkLlZzXxCcVvBbNnMm" + 
            "ЙйЦцКкНнГгШшЩщЗзХхЪъФфВвПпРрЛлДдЖжЧчСсМмТтЬьБб";
    }
}
