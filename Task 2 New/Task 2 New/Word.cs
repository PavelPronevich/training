using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_2_New
{
    class Word<X> : IList<X> where X : Letter
    {
        private List<X> word = new List<X>();

        //public List<X> Word
        //{
        //    get
        //    {
        //        return word;
        //    }
        //}


        public int IndexOf(X item)
        {
            return word.IndexOf(item);
        }

        public void Insert(int index, X item)
        {
            word.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            word.RemoveAt(index);
        }

        public X this[int index]
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

        public void Add(X item)
        {
            word.Add(item);
        }

        public void Clear()
        {
            word.Clear();
        }

        public bool Contains(X item)
        {
            return word.Contains(item);
        }

        public void CopyTo(X[] array, int arrayIndex)
        {
            word.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { throw new NotImplementedException(); }
        }

        public bool IsReadOnly
        {
            get { throw new NotImplementedException(); }
        }

        public bool Remove(X item)
        {
            return word.Remove(item);
        }


        public IEnumerator<X> GetEnumerator()
        {
            return word.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        override public string ToString()
        {
            StringBuilder wordString=new StringBuilder();
            foreach (X item in word)
            {
                wordString.Append(item.ToString());
            }
           // Console.WriteLine(wordString.ToString());
            return wordString.ToString();
        }
    }
}
