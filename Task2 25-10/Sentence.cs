using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2_25_10
{
    class Sentence
    {
        public List<ISentencePart> Value;
        public Sentence(List<ISentencePart> list)
        {
            this.Value = list;
            int number=0;
            foreach(ISentencePart item in list)
            {
                if (item.GetType()== typeof(Word))
                    number++;
            }
            NumberOfWords=number;
            
        }
       public int NumberOfWords{get; private set;}

       override public string ToString()
        {
            StringBuilder wordString = new StringBuilder();
            foreach (ISentencePart item in Value)
            {
                wordString.Append(item.ToString());
            }
            //for (int i = 0; i < this.Value.Count; i++)
            //{
            //    wordString.Append(Value[i].ToString());
            //}

            return wordString.ToString();
        }

       

      /* public SentenceType TypeOfSentence 
       { 
           get
           {
           if ((this.Value.Count-1).)
           }
           ; private set; }
       */ 
    }
}
