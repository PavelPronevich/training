﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_2_New
{
    class Text<X> : IList<X> where X : Sentance
    {
        private List<X> sentance = new List<X>();

        //public List<X> Word
        //{
        //    get
        //    {
        //        return word;
        //    }
        //}


        public int IndexOf(X item)
        {
            return sentance.IndexOf(item);
        }

        public void Insert(int index, X item)
        {
            sentance.Insert(index, item);
        }

        public void RemoveAt(int index)
        {
            sentance.RemoveAt(index);
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
            sentance.Add(item);
        }

        public void Clear()
        {
            sentance.Clear();
        }

        public bool Contains(X item)
        {
            return sentance.Contains(item);
        }

        public void CopyTo(X[] array, int arrayIndex)
        {
            sentance.CopyTo(array, arrayIndex);
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
            return sentance.Remove(item);
        }


        public IEnumerator<X> GetEnumerator()
        {
            return sentance.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            throw new NotImplementedException();
        }

        override public string ToString()
        {
            StringBuilder sentancedString = new StringBuilder();
            foreach (X item in sentance)
            {
                sentancedString.Append(item.ToString());
            }
            return sentancedString.ToString();
        }
    }
}