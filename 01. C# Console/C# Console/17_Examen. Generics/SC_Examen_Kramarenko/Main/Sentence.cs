using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Main
{
    class Sentence
    {
        protected List<string> Words;

        public Sentence() 
        {
            Words = new List<string>();
        }
        public Sentence(string str)
        {
            Words = new List<string>();
            Words.AddRange(str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
        }
        public Sentence(Sentence sourse)
        {
            Words = new List<string>();

            foreach (var item in sourse)
            {
                Words.Add(item.ToString());
            }
        }

        public void Add(string word)
        {
            Words.AddRange(word.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
        }
        public void Insert(int pos, string word)
        {
            if (pos < 0 || pos > Words.Count) throw new Exception("Wrong index");

            Words.InsertRange(pos, word.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
        }

        public void RemoveAt(int pos)
        {
            if (pos < 0 || pos >= Words.Count) throw new Exception("Wrong index");

            Words.RemoveAt(pos);
        }
        public void RemoveAll(string word)
        {
            while (Words.Remove(word)) { }
        }

        public void Set(string str)
        {
            Words.Clear();
            Words.AddRange(str.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
        }

        public string this[int pos]
        {
            get
            {
                if (pos < 0 || pos >= Words.Count) throw new Exception("Wrong index");
                return Words[pos];
            }
            set
            {
                if (pos < 0 || pos >= Words.Count) throw new Exception("Wrong index");

                Words.RemoveAt(pos);
                Words.InsertRange(pos, value.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries));
            }
        }
        public int Length
        {
            get => Words.Count;
        }

        public void Print()
        {
            foreach (var item in Words)
            {
                Console.Write($"{item} ");
            }
        }

        public virtual string ToString()
        {
            string result = string.Empty;

            foreach (var item in Words)
            {
                result += item + ' ';
            }

            return result;
        }
        public IEnumerator GetEnumerator()
        {
            foreach (var item in Words)
            {
                yield return item;
            }
        }

    }
}
