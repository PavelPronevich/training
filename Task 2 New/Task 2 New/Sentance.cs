using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_2_New
{
    class Sentance
    {

        public List<Word<Letter>> WordsOfSentance;
        public Intervals<Symbol> BeginSymbolsOfSentance;
        public List<Intervals<Symbol>> IntervalsBetweenWords;

        public Sentance(Intervals<Symbol> _BeginSymbolsOfSentance, List<Word<Letter>> _WordsOfSentance,
            List<Intervals<Symbol>> _IntervalsBetweenWords)
        {
            this.WordsOfSentance = _WordsOfSentance;
            this.BeginSymbolsOfSentance = _BeginSymbolsOfSentance;
            this.IntervalsBetweenWords = _IntervalsBetweenWords;
        }
        

        override public string ToString()
        {
            StringBuilder sentanceString = new StringBuilder();

            sentanceString.Append(BeginSymbolsOfSentance.ToString());

            //foreach (Symbol s in BeginSymbolsOfSentance)
            //{
                //Console.WriteLine(s.ToString());
              //  sentanceString.Append(s.ToString());
            //}
            
            //Console.WriteLine(sentanceString);

                for (int i=0;i<WordsOfSentance.Count; i++)
                {
                    //Console.WriteLine(WordsOfSentance[i].ToString());
                    sentanceString.Append(WordsOfSentance[i].ToString());
                    //Console.WriteLine(sentanceString);
                    sentanceString.Append(IntervalsBetweenWords[i].ToString());
                }
                
            return sentanceString.ToString();
        }
         
        public Symbol getLastSymbol()
        {

            return ((IntervalsBetweenWords[IntervalsBetweenWords.Count - 1])
                [(IntervalsBetweenWords[IntervalsBetweenWords.Count - 1]).Count-1]);
        }
        

    }
}
