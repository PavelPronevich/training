using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2_25_10
{
    public class Text<X> : IList<X> where X : Sentence
    {
        
        private List<X> text = new List<X>();
        //private object _contents = new object();
        //WordNew _contents;

        //private int _count;
        //public WordsCollecti()
        //{
        //    _count = 0;
        //}

       // public List<X> Text
       // {
       //     get
       //     {
       //         return text;
       //     }
       // }


        public int IndexOf(X item)
        {
            return text.IndexOf(item);
        }

        public void Insert(int index, X item)
        {
            text.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            text.RemoveAt(index);
        }

        public X this[int index]
        {
            get
            {
                return text[index];
            }
            set
            {
                text[index] = value;
            }
        }

        public void Add(X item)
        {

            text.Add(item);
        }

        public void Clear()
        {
            text.Clear();
        }

        public bool Contains(X item)
        {
            return text.Contains(item);
        }

        public void CopyTo(X[] array, int arrayIndex)
        {
            text.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return text.Count; }
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        public bool Remove(X item)
        {
            return text.Remove(item);
        }


        public IEnumerator<X> GetEnumerator()
        {
            return text.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return text.GetEnumerator();
        }

        override public string ToString()
        {
            StringBuilder wordString = new StringBuilder();
            foreach (X item in text)
            {
                wordString.Append(item.ToString());
            }
            
            return wordString.ToString();
        }

        public IEnumerable<Sentence> SortSentenceByWords()
        {
            var query = from item in text
                                               orderby item.NumberOfWords
                                               select item;
            return query;
        }

        public IEnumerable<Sentence> GetInterrogativeSentences()
        {
            var query = from item in text
                        where item.LastOrDefault().ToString().Equals("?")
                        select item;
            
            return query;
        }

        public void DeleteAllWordsOfLength(int LengthOfWordToDelete)
        {
            int  LengthOfWord = LengthOfWordToDelete;

            foreach (var item in text)
            {
                for (int i = 0; i < item._items.Count; i++)
                {
                    if (item._items[i].GetType() == typeof(Word))
                    {
                        if (((Word)item._items[i]).Length == LengthOfWord
                            && ((Word)item._items[i]).getFirstCharacter().IsConsonant)
                        {
                            item._items.RemoveRange(i, 1);
                        }
                    }

                }
                item.DelWhiteSpaces();
            }
            
        }


    }
}
