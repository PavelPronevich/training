using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextToSEntence
{
    
    class Program
    {
        
        static void Main(string[] args)
        {
                                

            //String TextFromFile = System.IO.File.ReadAllText(@"E:\epam\WriteText.txt");
            String TextFromFile = @"Console.WriteLine(Привет мир мир мир мир и еще раз мир).
Эти обучающие уроки предназна¬чены для всех, независимо от того, новичок вы в программировании или у вас уже есть обширный опыт программирования на других языках! Данный материал для тех, кто хочет изучить языки С/С++ от самых его основ до сложнейших конструкций.
Было ли это полезнодля вас?


C++ является языком програм¬мирования? Конечно. Знание этого языка программирования позволит вам управлять вашим компьютером на высшем уровне. В идеале вы сможете заста¬вить компьютер сделать всё, что сами захотите. Наш сайт поможет вам в освоении языка программирования C++? Может поможет, а может и нет.
";

            Console.WriteLine("             Original text\n");
            Console.WriteLine(TextFromFile);

            Text<Sentence> BookOfSentence =StringToText(TextFromFile);


            
            Console.WriteLine("\n\n\n             Sentences sorted by number of words\n");
            foreach (var item in BookOfSentence.SortSentenceByWords())
            {
                Console.WriteLine("{0} ({1} words)", item.ToString().Trim(), item.NumberOfWords);
            }


            Console.WriteLine("\n\n\n             Original text has next interrogative sentences:\n");
            foreach (var item in BookOfSentence.GetInterrogativeSentences())
            {
                Console.WriteLine("{0}", item.ToString().Trim());
            }

            int LengthOfWordToFind = 3;
            Console.WriteLine("\n\n            they have next words of length {0}:\n", LengthOfWordToFind);
            var query1 = BookOfSentence.GetInterrogativeSentences().SelectMany(x => x.Where(y => y is Word
                && (y as Word).Length == LengthOfWordToFind).Cast<Word>()).Distinct<Word>(new WordComparer());

            foreach (var item in query1)
            {
                Console.Write(item + " ");
            }
                      
            
            int LengthOfWordToDelete=3;

            BookOfSentence.DeleteAllWordsOfLength(LengthOfWordToDelete);

             Console.WriteLine("\n\n            Original text after delete of all words with \n"+
            "            length of {0} words and first consonant character.", LengthOfWordToDelete);
             Console.WriteLine(BookOfSentence);



             

            string stringToPaste = "Мама мыла раму";
            Text<Sentence> BookOfPaste = StringToText(stringToPaste);
            List<ISentencePart> sentenceToPaste=new List<ISentencePart>();
            foreach (var item in BookOfPaste)
            { sentenceToPaste.AddRange(item._items); }
            int lengthOfWordsToReaplace=4;



            Console.WriteLine("\n\n            In this sentence");
            Console.WriteLine(BookOfSentence[2]);

            BookOfSentence[2].ReplaceAll(lengthOfWordsToReaplace, sentenceToPaste);
            Console.WriteLine("\n\n            all words with {0} characters were replaced by {1}",
                lengthOfWordsToReaplace, BookOfPaste);

            Console.WriteLine(BookOfSentence[2]);
            Console.ReadKey();


            


        

    }
        static public Text<Sentence> StringToText(string textString)
        {
            string SymbolsOfEndSentance = ".!?";
            //string StringIntervalsBetweenWords = " .!?,;:-(){}[]|\"";
            string Letters = "1234567890ЁёЙЦУКЕНГШЩЗХЪФЫВАПРОЛДЖЭЯЧСМИТЬБЮйцукенгшщзхъфывапролджэячсмитьбю" +
                "QWERTYUIOPASDFGHJKLZXCVBNMqwertyuiopasdfghjklzxcvbnm@#$%^&*/+";
            List<Character> CharactersObject = new List<Character>();
            foreach (char item in Letters)
            {
                CharactersObject.Add(new Character(item));
            }

            string TextString = textString;
            //удаление форматирования
            TextString = TextString.Replace("\n", " ");
            TextString = TextString.Replace("\t", " ");
            TextString = TextString.Replace("\r", " ");
            TextString = TextString.Replace("¬", "");
            //удаление парных пробелов
            string whiteSpace = "  ";
            while (TextString.Contains(whiteSpace))
            {
                TextString = TextString.Remove(TextString.IndexOf(whiteSpace), 1);
            }

            Text<Sentence> Book = new Text<Sentence>();
            List<Character> varWord = new List<Character>();
            //List<Symbol> varCombSymbols = new List<Symbol>();
            List<ISentencePart> varSentance = new List<ISentencePart>();



            for (int i = 0; i < TextString.Length; i++)
            {
                if (Letters.Contains(TextString[i]))
                {
                    varWord.Add(CharactersObject[Letters.IndexOf(TextString[i])]);
                }
                else
                {
                    if (varWord.Count != 0)
                    {
                        varSentance.Add(new Word(new List<Character>(varWord)));
                        varWord.Clear();
                    }
                    varSentance.Add(new Symbol(TextString[i]));

                    if (SymbolsOfEndSentance.Contains(TextString[i]))
                    {
                        Book.Add(new Sentence(new List<ISentencePart>(varSentance)));
                        varSentance.Clear();
                    }


                }

                if ((i == TextString.Length - 1) && (Letters.Contains(TextString[i])))
                {
                    varSentance.Add(new Word(new List<Character>(varWord)));
                    Book.Add(new Sentence(new List<ISentencePart>(varSentance)));
                }

            }
            return Book;
        }
    }
}
