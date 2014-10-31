using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task2_25_10
{
    public class Sentence : ICollection<ISentencePart>
    {
        public List<ISentencePart> _items=new List<ISentencePart>();
        public Sentence(List<ISentencePart> list)
        {
            this._items = list;
        }

        public int NumberOfWords
        {
            get
            {
                return _items.Count(x=>x is Word);
            }
        }


        /*public IEnumerable<Word> GetWords()
        {
           return _items.Where(x=>x is Word).Select( x=> x as Word);
        }
         */
        

       override public string ToString()
        {
            StringBuilder wordString = new StringBuilder();
            foreach (ISentencePart item in _items)
            {
                wordString.Append(item.ToString());
            }
            return wordString.ToString();
        }

        public void DeleteWord(string wordToDelete)
        {
            for (int i=0; i< this._items.Count;i++)
            {
                if (this._items[i].ToString().Equals(wordToDelete))
                {
                    this._items.RemoveRange(i, 1);
                }
            }
        }
       
        public void DelWhiteSpaces()
        {
            string separators = " .!?,;:)]}";
            for (int i=0;i<_items.Count-1;i++)
                if (_items[i].GetType()== typeof(Symbol) && (_items[i].ToString().Equals(" ") 
                    && separators.Contains(_items[i + 1].ToString())))
                {
                    _items.RemoveRange(i--, 1);
                }
        }


     
       

        public void Add(ISentencePart item)
        {
            _items.Add(item);
        }

        public void Clear()
        {
            _items.Clear();
        }

        public bool Contains(ISentencePart item)
        {
            return _items.Contains(item);
        }

        public void CopyTo(ISentencePart[] array, int arrayIndex)
        {
            _items.CopyTo(array, arrayIndex);
        }

        public int Count
        {
            get { return _items.Count; }
        }

        public bool IsReadOnly
        {
            get { return false; }
        }

        public bool Remove(ISentencePart item)
        {
            return _items.Remove(item);
        }

        public IEnumerator<ISentencePart> GetEnumerator()
        {
           return _items.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }





        /*public void ReplaceAll(Func<ISentencePart,bool> condition, IEnumerable<ISentencePart> newItems)
        {
            _items.Skip()
            ISentencePart current = _items.FirstOrDefault(condition);
            do
            {
                if (current!=null)
                {

                }
            }
            while()
        }
         */
    }
}
