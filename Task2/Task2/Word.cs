using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2
{
    class Word
    {
        public string Value { get; private set; }
        public int Lenth { get; private set; }
        public string NachSymbol { get; private set; }
        public string KonechSymbol { get; private set; }
        public string ToType { get; private set; }

        public Word(string nachSymbol, string value, string konechSymbol)
        {
            this.Value = value;
            this.NachSymbol = nachSymbol;
            this.KonechSymbol = konechSymbol;
            this.ToType = nachSymbol + value + konechSymbol;
            this.Lenth = value.Length;
        }
    }
}
