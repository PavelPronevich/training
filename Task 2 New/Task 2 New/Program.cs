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

            string StringSymbolsOfEndSentance = ".!?";
            string StringIntervalsBetweenWords = " ,;:-(){}[]|\"";
            string Letters = "1234567890ЁёЙЦУКЕНГШЩЗХЪФЫВАПРОЛДЖЭЯЧСМИТЬБЮйцукенгшщзхъфывапролджэячсмитьбю" +
                "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm@#$%^&*/";

            Text<Sentance> Book=new Text<Sentance>();
            
            List<Word<Letter>> wordsOfSentance=new List<Word<Letter>>();
            Intervals<Symbol> beginSymbolsOfSentance = new Intervals<Symbol>();
           // List<Symbol> endSymbolsOfSentance;
            List<Intervals<Symbol>> intervalsBetweenWords = new List<Intervals<Symbol>>();

            Word<Letter> varWord=new Word<Letter>();
            Intervals<Symbol> varIntervalBetweenWords = new Intervals<Symbol>();
            bool haveBeginSentance=false;
            
            bool haveWord=false;
            //bool haveIntervalBetweenWords=false;

            int i=0;

            while (i<A.Length)
            {
                if (Letters.Contains(A[i]))
                {
                    if (haveBeginSentance==false)
                    {
                        haveBeginSentance=true;
                    }
                    if (haveWord==true)
                    {
                        wordsOfSentance.Add(varWord);
                        Console.Write(varWord);
                        Console.Write(varIntervalBetweenWords);

                        varWord.Clear();
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
                    
                    wordsOfSentance.Add(varWord);

                   Console.WriteLine(varWord);
                   

                    varIntervalBetweenWords.Add(new Symbol (A[i]));
                    Console.WriteLine(varIntervalBetweenWords);

                    intervalsBetweenWords.Add(varIntervalBetweenWords);
                    //Console.Write(varWord);
                    //Console.WriteLine(varIntervalBetweenWords);
                    //Console.WriteLine(varIntervalBetweenWords);
                    i++;
                   
                                     
                    
                    //Console.WriteLine(varIntervalBetweenWords.ToString());
                    //Sentance set = new Sentance(beginSymbolsOfSentance, wordsOfSentance, intervalsBetweenWords);
                    
                    Book.Add(new Sentance(beginSymbolsOfSentance, wordsOfSentance,intervalsBetweenWords));

                    foreach (Intervals<Symbol> t in intervalsBetweenWords)
                    {
                        Console.WriteLine(t);
                    }

                    beginSymbolsOfSentance.Clear();
                    wordsOfSentance.Clear();
                    intervalsBetweenWords.Clear();

                    varWord.Clear();

                    varIntervalBetweenWords.Clear();

                    haveBeginSentance = false;
                    haveWord = false;


                }

            }

            Console.ReadLine();


            /*Letter a=new Letter('Ь');
            Console.WriteLine(a.Value);
            Console.WriteLine(a.IsConsonant);
            Console.WriteLine(a.ToString());

            Word<Letter> slovo = new Word<Letter>();
            slovo.Add(new Letter('C'));
            slovo.Add(new Letter('л'));
            slovo.Add(new Letter('о'));
            slovo.Add(new Letter('в'));
            slovo.Add(new Letter('о'));

            Console.WriteLine(slovo);

            Console.ReadKey();
            String A = System.IO.File.ReadAllText(@"E:\epam\WriteText.txt"+" ");
             */           
            //I ToString, количество элементов
            //символ (знак конца предложения, знак конца слова, буква)
            //слово string+начало слова+конец слова
            // предложение list<слово> + символ конца предложения
            // глава 
            // book list<предложение>
            

          /*
            
            int i,j,k,number;
            number = 0;
            List<string> e=new List<string>(); 

            while (A.IndexOf(". ")!=A.LastIndexOf(". "))
            {
                if (A.Contains(". "))
                {
                    i = A.IndexOf(". ");
                    if (A.Contains("! "))
                    {
                        j = A.IndexOf("! ");

                        if (A.Contains("? "))
                        {
                            k = A.IndexOf("? ");
                            if ((i < j) && (i < k))
                            {
                                e.Add(A.Substring(0,i));
                                number = i + 2;
                            }
                            if ((j < i) && (j < k))
                            {
                                e.Add(A.Substring(0,j));
                                number += j + 2;
                            }
                            if ((k < i) && (k < j))
                            {
                                e.Add(A.Substring(0,k));
                                number = k + 2;
                            }
                        }
                        else
                        {
                            if (i < j)
                            {
                                e.Add(A.Substring(0,i));
                                number = i + 2;
                            }
                            else
                            {
                                e.Add(A.Substring(0,j));
                                number = j + 2;
                            }
                        }

                    }
                    else
                    {
                        e.Add(A.Substring(0,i));
                        number = i + 2;
                    }
                }
                A = A.Remove(0, number);
               
            }
            
            foreach(string item in e)
            {
                Console.WriteLine(item);
            }
            
            
            Console.ReadKey();
            */
            


           
        }
    }
}
