using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_2_New
{
    class Program
    {
        static void Main(string[] args)
        {
            /* Symbol e = new Symbol('r');
             Symbol r = new Symbol('w');
             Console.WriteLine(e);
             */

            String A = System.IO.File.ReadAllText(@"E:\epam\WriteText.txt");
            //удаление форматирования
            A = A.Replace("\n", " ");
            A = A.Replace("\t", " ");
            A = A.Replace("\r", " ");

            //удаление парных пробелов
            string prob = "  ";
            while (A.Contains(prob))
            {
                A = A.Remove(A.IndexOf(prob), 1);
            }

            Console.WriteLine(A);
            Console.WriteLine(A.Length);

            string StringSymbolsOfEndSentance = ".!?";
            string StringIntervalsBetweenWords = " ,;:-(){}[]|\"";
            string Letters = "1234567890ЁёЙЦУКЕНГШЩЗХЪФЫВАПРОЛДЖЭЯЧСМИТЬБЮйцукенгшщзхъфывапролджэячсмитьбю" +
                "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm@#$%^&*/";

            Text<Sentance> Book = new Text<Sentance>();

            WordsCollection<WordNew> wordsOfSentance = new WordsCollection<WordNew>();

            List<Symbol> beginSymbolsOfSentance=new List<Symbol>();
            IntervalsCollection<IntervalsNew> intervalsBetweenWords = new IntervalsCollection<IntervalsNew>();

            List<Letter> varWord=new List<Letter>();
            List<Symbol> varIntervalBetweenWords=new List<Symbol>();
            bool haveBeginSentance = false;
            bool haveWord = false;
           
            //bool haveIntervalBetweenWords=false;

            int i = 0;

            while (i < A.Length)
            {
                if (Letters.Contains(A[i]))
                {
                    if (haveBeginSentance == false)
                    {
                        haveBeginSentance = true;
                    }
                    else
                    {
                        intervalsBetweenWords.Add(new IntervalsNew(varIntervalBetweenWords));//доб нов интервал
                        varIntervalBetweenWords.Clear();// 
                        haveWord = false;
                    }
                    varWord.Add(new Letter(A[i++]));
                }
                
                if (StringIntervalsBetweenWords.Contains(A[i]))
                {
                    if (haveBeginSentance == false)
                    {
                        beginSymbolsOfSentance.Add(new Symbol(A[i++]));
                    }
                    else
                    {
                        haveWord = true;
                        wordsOfSentance.Add(new WordNew(varWord));//доб нов интервал
                        varWord.Clear();// 
                        
                        varIntervalBetweenWords.Add(new Symbol(A[i++]));
                    }
                }

                if(StringSymbolsOfEndSentance.Contains(A[i]))
                {
                    varIntervalBetweenWords.Add(new Symbol(A[i++]));
                    intervalsBetweenWords.Add(new IntervalsNew(varIntervalBetweenWords));//доб нов интервал
                    varIntervalBetweenWords.Clear();// 

                    if (haveWord==false)
                    {
                        wordsOfSentance.Add(new WordNew(varWord));//доб нов интервал
                        varWord.Clear();// 
                        haveWord = true;
                    }

                    Sentance st = new Sentance(new IntervalsNew(beginSymbolsOfSentance), wordsOfSentance, intervalsBetweenWords);
                    Book.Add(st);
                    Console.WriteLine(st);

                    beginSymbolsOfSentance.Clear();
                    wordsOfSentance.Clear();
                    intervalsBetweenWords.Clear();
                }
                
            }


            /*
                if (haveBeginSentance==false)
                {
                    haveBeginSentance=true;
                }
                if (haveWord==true)
                {
                    //wordsOfSentance.Add(new WordNew(varWord));
                    //Console.Write(varWord);
                    //Console.Write(varIntervalBetweenWords);

                    //varWord.Clear();
                    intervalsBetweenWords.Add(varIntervalBetweenWords);
                        
                    varIntervalBetweenWords.Clear();
                    haveWord=false;
          //        haveIntervalBetweenWords=false;
                }

                varWord.Add(new Letter(A[i++]));
                    
                continue;
            }
                
            if (StringIntervalsBetweenWords.Contains(A[i]))
            {
                if (haveBeginSentance==false)
                {
                    beginSymbolsOfSentance.Add(new Symbol(A[i]));
                    i++;
                    continue;
                }
                else 
                {
                    haveWord=true;

                    varIntervalBetweenWords.Add(new Symbol(A[i]));
                    i++;
                }
            }
            if (StringSymbolsOfEndSentance.Contains(A[i]))
            {
                   

                wordsOfSentance.Add(new WordNew(varWord));
                    
                varIntervalBetweenWords.Add(new Symbol (A[i]));
                //Console.WriteLine(varIntervalBetweenWords);

                intervalsBetweenWords.Add(varIntervalBetweenWords);
                i++;



                Sentance st = new Sentance(beginSymbolsOfSentance, wordsOfSentance, intervalsBetweenWords);
                Book.Add(st);
                //Book.Add(new Sentance(beginSymbolsOfSentance, wordsOfSentance,intervalsBetweenWords));
                //Console.Write(beginSymbolsOfSentance);
                foreach( WordNew item in wordsOfSentance)
                {
                    Console.Write(item);
                }
                //Console.Write(wordsOfSentance);
                //Console.WriteLine(intervalsBetweenWords);

                //Console.WriteLine(st);
                    
                //foreach (Intervals<Symbol> t in intervalsBetweenWords)
                //{
                //    Console.WriteLine(t);
                //}

                beginSymbolsOfSentance.Clear();
                wordsOfSentance.Clear();
                intervalsBetweenWords.Clear();

                varWord.Clear();

                varIntervalBetweenWords.Clear();

                haveBeginSentance = false;
                haveWord = false;


            }    
                   
        }*/

            Console.ReadLine();

    }
    }
}

