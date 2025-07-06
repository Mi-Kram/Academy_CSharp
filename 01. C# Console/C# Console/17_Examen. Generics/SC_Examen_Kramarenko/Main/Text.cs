using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Main
{
    class Text
    {
        protected List<Sentence> text;

        public Text() 
        {
            text = new List<Sentence>();
        }
        public Text(string str)
        {
            if (str.Length == 0) throw new Exception("Empty string");
            text = new List<Sentence>();
            Parse(str);
        }
        protected Text(Text source)
        {
            text = new List<Sentence>();

            foreach (var item in source.text)
            {
                text.Add(new Sentence(item));
            }
        }

        protected void Parse(string str)
          {
            if (".!?".IndexOf(str[str.Length - 1]) == -1) str += '.';

            MatchCollection m = Regex.Matches(str, @"\.|\!|\?");
            if(m.Count == 1)
            {
                text.Add(new Sentence(str));
                return;
            }

            int start = 0;
            string result = string.Empty, tmp = string.Empty;

            if(m.Count != 0)
            {
                result += str.Remove(m[0].Index + 1);
                start = m[0].Index + 1;
            }

            for (int i = 1; i < m.Count-1; i++)
            {
                tmp = str.Remove(m[i].Index + 1).Remove(0, start);
                start = m[i].Index+1;

                if (tmp.Length == 1) result += tmp;
                else
                {
                    text.Add(new Sentence(result));
                    result = tmp;
                }
            }

            tmp = str.Remove(0, start);
            if (tmp.Length != 1)
            {
                text.Add(new Sentence(result));
                result = string.Empty;
            }

            result += tmp;
            text.Add(new Sentence(result));
        }
        private void ParsePos(int pos, string str)
          {
            if (".!?".IndexOf(str[str.Length - 1]) == -1) str += '.';

            MatchCollection m = Regex.Matches(str, @"\.|\!|\?");
            if (m.Count == 1)
            {
                text.Insert(pos++, new Sentence(str));
                return;
            }

            int start = 0;
            string result = string.Empty, tmp = string.Empty;

            if (m.Count != 0)
            {
                result += str.Remove(m[0].Index + 1);
                start = m[0].Index + 1;
            }

            for (int i = 0; i < m.Count-1; i++)
            {
                tmp = str.Remove(m[i].Index + 1).Remove(0, start);
                start = m[i].Index+1;

                if (tmp.Length == 1) result += tmp;
                else
                {
                    if (result.Length != 0)
                    {
                        text.Insert(pos++, new Sentence(result));
                        result = tmp;
                    }
                }
            }

            tmp = str.Remove(0, start);
            if (tmp.Length != 1)
            {
                text.Insert(pos++, new Sentence(result));
                result = string.Empty;
            }

            result += tmp;
            text.Insert(pos, new Sentence(result));
        }

        public void Add(string str)
        {
            if (str.Length == 0) throw new Exception("Empty string");
            Parse(str);
        }
        public void Add(Sentence str)
        {
            if (str.Length == 0) throw new Exception("Empty string");
            Parse(str.ToString());
        }

        public void Insert(int pos, string str)
        {
            if (pos < 0 || pos > text.Count) throw new Exception("Wrong index");
            if (str.Length == 0) throw new Exception("Empty string");

            ParsePos(pos, str);
        }
        public void Insert(int pos, Sentence str)
        {
            if (pos < 0 || pos > text.Count) throw new Exception("Wrong index");
            if (str.Length == 0) throw new Exception("Empty string");

            ParsePos(pos, str.ToString());
        }

        public void RemoveAt(int pos)
        {
            if (pos < 0 || pos >= text.Count) throw new Exception("Wrong index");
            text.RemoveAt(pos);
        }

        public void Set(string str)
        {
            if (str.Length == 0) throw new Exception("Empty string");
            text.Clear();
            Parse(str);
        }
        public void Print()
        {
            foreach (var item in text)
            {
                Console.WriteLine(item.ToString());
            }
        }

        public string this[int s]
        {
            get
            {
                if (s < 0 || s >= text.Count) throw new Exception("Wrong index");

                return text[s].ToString();
            }
            set
            {
                if (s < 0 || s >= text.Count) throw new Exception("Wrong index");

                RemoveAt(s);
                Insert(s, value);
            }
        }
        public string this[int s, int w]
        {
            get
            {
                if (s < 0 || s >= text.Count) throw new Exception("Wrong index");
                if(w < 0 || w >= text[s].Length) throw new Exception("Wrong index");

                return text[s][w];
            }
            set
            {
                if (s < 0 || s >= text.Count) throw new Exception("Wrong index");
                if (w < 0 || w >= text[s].Length) throw new Exception("Wrong index");

                text[s].RemoveAt(w);
                text[s].Insert(w, value);
            }
        }
        public int Length
        {
            get => text.Count;
        }

        public int WordsInSentence(int pos)
        {
            if (pos < 0 || pos >= text.Count) throw new Exception("Wrong index");

            return text[pos].Length;
        }

        public IEnumerator GetEnumerator()
        {
            foreach (var item in text)
            {
                yield return item.ToString();
            }
        }
        public virtual string ToString()
        {
            string result = string.Empty;

            foreach (var item in text)
            {
                result += item.ToString() + "\r\n";
            }

            return result;
        }
    }
}
