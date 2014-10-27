using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2_25_10
{
    class Character
    {
        public char Char;
        public bool IsConsonant { get; private set; }
        public Character(char character)
        {
            this.Char = character;

            if (ConsonantCharactersString.Contains(this.Char))
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
            return this.Char.ToString();
        }
        
        
        public string ConsonantCharactersString = "QqWwRrTtPpSsDdFfGgHhJjKkLlZzXxCcVvBbNnMm" + 
            "ЙйЦцКкНнГгШшЩщЗзХхЪъФфВвПпРрЛлДдЖжЧчСсМмТтЬьБб";
    }
}
