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

            Letter a=new Letter('Ь');
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
                        
            //I ToString, количество элементов
            //символ (знак конца предложения, знак конца слова, буква)
            //слово string+начало слова+конец слова
            // предложение list<слово> + символ конца предложения
            // глава 
            // book list<предложение>
            

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
            
            // выделение предложений (повествовательных, вопросительных, побудительных)

            
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
            
            


           
        }
    }
}
