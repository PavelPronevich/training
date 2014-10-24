using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_2_New
{
    class Sentance
    {

        private WordsCollection<WordNew> WordsOfSentance = new WordsCollection<WordNew>();
        private IntervalsNew BeginSymbolsOfSentance;
        private IntervalsCollection<IntervalsNew> IntervalsBetweenWords = new IntervalsCollection<IntervalsNew>();

        public Sentance(IntervalsNew _BeginSymbolsOfSentance, WordsCollection<WordNew> _WordsOfSentance,
           IntervalsCollection<IntervalsNew> _IntervalsBetweenWords)
        {
            this.WordsOfSentance = _WordsOfSentance;
            this.BeginSymbolsOfSentance = _BeginSymbolsOfSentance;
            this.IntervalsBetweenWords = _IntervalsBetweenWords;
        }
        

        override public string ToString()
        {
            StringBuilder sentanceString = new StringBuilder();

            sentanceString.Append(BeginSymbolsOfSentance);

            //foreach (Symbol s in BeginSymbolsOfSentance)
            //{
                //Console.WriteLine(s.ToString());
              //  sentanceString.Append(s.ToString());
            //}
            
            //Console.WriteLine(sentanceString);

                //for (int i=0;i<WordsOfSentance.Count; i++)
            int i = 0;        
            while (i<WordsOfSentance.Count)
               {
               //   //Console.WriteLine(WordsOfSentance[i].ToString());
                    sentanceString.Append(WordsOfSentance[2].ToString());
                    i++;
                    //Console.WriteLine(sentanceString);
                    sentanceString.Append(IntervalsBetweenWords[i].ToString());
                }
                
            return sentanceString.ToString();
        }
         
        //public Symbol getLastSymbol()
        //{
        //
        //    return ((IntervalsBetweenWords[IntervalsBetweenWords.Count - 1])
        //        [(IntervalsBetweenWords[IntervalsBetweenWords.Count - 1]).Count-1]);
        //}
        

    }
}
