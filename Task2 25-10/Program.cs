using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2_25_10
{
    public enum SentenceType : byte
    {
        Declarative = 1,
        interrogative = 2,
        exclamatory = 3
    }

    class Program
    {
        
        static void Main(string[] args)
        {
           
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

            //Console.WriteLine(A);
            //Console.WriteLine(A.Length);
            
            string StringSymbolsOfEndSentance = ".!?";
            //string StringIntervalsBetweenWords = " .!?,;:-(){}[]|\"";
            string Letters = "1234567890ЁёЙЦУКЕНГШЩЗХЪФЫВАПРОЛДЖЭЯЧСМИТЬБЮйцукенгшщзхъфывапролджэячсмитьбю" +
                "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm@#$%^&*/";

            Text<Sentence> Book = new Text<Sentence>();
            List<Character> varWord=new List<Character>();
            List<Symbol> varCombSymbols=new List<Symbol>();
            List<ISentencePart> varSentance = new List<ISentencePart>();
            

            for (int i = 0; i < A.Length;i++)
            {
                if (Letters.Contains(A[i]))
                {
                    varWord.Add(new Character(A[i]));
                    if (varCombSymbols.Count!=0)
                    {
                        varSentance.Add(new CombinationSymbol(new List<Symbol>(varCombSymbols)));
                        varCombSymbols.Clear();
                       
                    }
                }
                else
                {
                    
                    if (varWord.Count != 0)
                    {
                        
                        varSentance.Add(new Word(new List<Character>(varWord)));
                        varWord.Clear();

                       
                    }
                    if (StringSymbolsOfEndSentance.Contains(A[i]))
                    {
                        varSentance.Add(new CombinationSymbol(new List<Symbol>(varCombSymbols)));
                        varCombSymbols.Clear();
                        varCombSymbols.Add(new Symbol(A[i]));
                        varSentance.Add(new CombinationSymbol(new List<Symbol>(varCombSymbols)));
                        varCombSymbols.Clear();
                        
                        /*byte type1;
                        switch (A[i])
                        {
                            case '.':
                                type1 = SentenceType.Declarative;
                                break;
                            case '!':
                                Console.WriteLine("Case 2");
                                break;
                            case '?':
                                break;
                        }
                         */
 
                        Book.Add(new Sentence(new List<ISentencePart>(varSentance)));
                        varSentance.Clear();
                    }
                    else
                    {
                        varCombSymbols.Add(new Symbol(A[i]));
                    }
                }
                //Console.WriteLine(new Sentance(varSentance));

            }

            //var numQuery =
            //from item in Book
            //where item.NumberOfWords
            //select num;

            var query = from item in Book
                        orderby item.NumberOfWords
                        select item;

            foreach (var item in query)
            {
                Console.WriteLine("{0} ({1} words)", item.ToString().Trim(), item.NumberOfWords);
            }

            
            Console.WriteLine(Book);
            //Console.WriteLine(Book[2].NumberOfWords);
            foreach (var item in Book)
            {
                Console.WriteLine(item);
            }
            Console.ReadKey();




            
            /*Проверка работы класса Sentance
            List<Character> list=new List<Character>();
            list.Add(new Character('C'));
            list.Add(new Character('л'));
            list.Add(new Character('о'));
            list.Add(new Character('в'));
            list.Add(new Character('о'));
            Word e0 = new Word(list);
            
            List<Symbol> list1 = new List<Symbol>();
            list1.Add(new Symbol('"'));
            list1.Add(new Symbol(' '));
            list1.Add(new Symbol('('));
            list1.Add(new Symbol('\''));
            CombinationSymbol p0 = new CombinationSymbol(list1);

            
            List<Character> list2 = new List<Character>();
            list2.Add(new Character('в'));
            list2.Add(new Character('т'));
            list2.Add(new Character('о'));
            list2.Add(new Character('р'));
            list2.Add(new Character('о'));
            list2.Add(new Character('е'));
            Word e1 = new Word(list2);

            List<Symbol> list3 = new List<Symbol>();
            list3.Add(new Symbol('.'));
            CombinationSymbol p1 = new CombinationSymbol(list3);

            List<ISentancePart> sen=new List<ISentancePart>();
            sen.Add(e0);
            sen.Add(p0);
            sen.Add(e1);
            sen.Add(p1);
            Sentance sentance = new Sentance(sen);
                       
            Console.WriteLine(sen[0]);
            Console.WriteLine(sen[1]);
            Console.WriteLine(sen[2]);
            Console.WriteLine(sen[3]);
            
            StringBuilder wordString = new StringBuilder();

            foreach (ISentancePart item in sen)
            {
                wordString.Append(item.ToString());
                
            }
            Console.WriteLine(wordString);

            Console.WriteLine(sentance.ToString());
            Console.ReadKey();
             */

        }
    }
}
