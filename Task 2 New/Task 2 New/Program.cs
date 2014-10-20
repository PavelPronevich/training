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
            String A = System.IO.File.ReadAllText(@"E:\epam\WriteText.txt"+" ");

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
            int index=0;
            number = 0;
            string e;

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
                                Console.WriteLine(A.Substring(0,i));
                                number = i + 2;
                            }
                            if ((j < i) && (j < k))
                            {
                                Console.WriteLine(A.Substring(0,j));
                                number += j + 2;
                            }
                            if ((k < i) && (k < j))
                            {
                                Console.WriteLine(A.Substring(0,k));
                                number = k + 2;
                            }
                        }
                        else
                        {
                            if (i < j)
                            {
                                Console.WriteLine(A.Substring(0,i));
                                number = i + 2;
                            }
                            else
                            {
                                Console.WriteLine(A.Substring(0,j));
                                number = j + 2;
                            }
                        }

                    }
                    else
                    {
                        Console.WriteLine(A.Substring(0,i));
                        number = i + 2;
                    }
                }
                A = A.Remove(0, number);
                
            }
            Console.ReadKey();
           
            


           
        }
    }
}
