using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    class Letter: Symbol
    {
        public bool IsUpperCaseLetters { get; set; }
        public bool IsVowelLetter { get; set; }

        string stringOfUpperCaseLetters = "QWERTYUIOPASDFGHJKLZXCVBNMЁЙЦУКЕНГШЩЗХЪФЫВАПРОЛДЖЭЯЧСМИТЬБЮ";
        string stringOfVawelLetters = "АаЕеЁёИиОоУуЫыЭэЮюЯяAaEeIiOoUuYy";
        public Letter(char value): base(value)
        {
            if (stringOfUpperCaseLetters.Contains(value.ToString()))
            {
                this.IsUpperCaseLetters = true;
            }
            else
            {
                this.IsUpperCaseLetters = false;
            }

            if (stringOfVawelLetters.Contains(value.ToString()))
            {
                this.IsVowelLetter = true;
            }
            else
            {
                this.IsVowelLetter = false;
            }
        }


    }
}