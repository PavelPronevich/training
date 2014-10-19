using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Task2
{
    class Program
    {
        static void Main(string[] args)
        {
            Letter sym = new Letter('R');
            Console.WriteLine(sym.Value);
            Console.WriteLine(sym.IsUpperCaseLetters);
            Console.WriteLine(sym.IsVowelLetter);
            

            char er='w';
            Console.WriteLine(er.GetHashCode());
            Word proba = new Word("(", "легко", "mb)");
            Console.WriteLine(proba.ToType);

            Console.ReadKey();

            String A = System.IO.File.ReadAllText(@"E:\epam\WriteText.txt")+" ";
            Console.WriteLine(A.IndexOf(". "));       
            
            
            Console.ReadKey();

            //Console.WriteLine("Contents of WriteText.txt = {0}", text);
            //Console.ReadKey();
            //string[] lines = System.IO.File.ReadAllLines(@"E:\epam\WriteText.txt");
            //Console.WriteLine(lines[0] + lines[1] + lines[2]);
            //Console.ReadKey();
            //Console.WriteLine(lines[1]);
            //Console.ReadKey();
            //Console.WriteLine(lines[2]);
            //Console.ReadKey();
            //Console.WriteLine(lines[3]);
            //Console.ReadKey();
            //Console.WriteLine("\t" + lines[3]);
            //Console.ReadKey();
            //string[] sentences = text.Split(new char[] { '.', '?', '!' });

            //string A="Структура. программы. C# Main() и аргументы командной! строки. (Руководство по программированию на C#)";

            /*StringBuilder e = new StringBuilder();
            List<string> r = new List<string>();
            int i=-1;
            
            while (++i < A.Length)
            {
                
                {
                }
                if ( (A[i].Equals('.') && A[i + 1].Equals(' ')) || 
                      (A[i].Equals('!') && A[i + 1].Equals(' ')) ||
                      (A[i].Equals('?') && A[i + 1].Equals(' ')))
                {
                    e.Append(A[i]);
                    r.Add(e.ToString()); 
                    Console.WriteLine(e);
                    e.Clear();
                    continue;
                }

                 if (A[i].Equals(' ') && A[i + 1].Equals(' '))
                {
                    continue;
                }
                elseif ((A[i].CompareTo('\t') == 0) || (A[i].CompareTo('\n') == 0) || (A[i].CompareTo('\r') == 0)))
                {
                    e.Append(' ');
                    if (A[i+1].CompareTo(' ') == 0)
                    {
                        i+=2;
                    }
                    else{
                        i++;
                    }
                    
                }
                else
                {
                    e.Append(A[i]);
                    i++;
                }
            }
            
            using (System.IO.StreamWriter file = new System.IO.StreamWriter(@"E:\epam\WriteText2.txt"))
        {
            foreach (string line in r)
            {
                // If the line doesn't contain the word 'Second', write the line to the file.
                if (!line.Contains("Second"))
                {
                    file.WriteLine(line);
                }
            }
        }
           
            // int a,b;
            //StringBuilder w;
           
        */    
            
            
            
        }
    }
}
