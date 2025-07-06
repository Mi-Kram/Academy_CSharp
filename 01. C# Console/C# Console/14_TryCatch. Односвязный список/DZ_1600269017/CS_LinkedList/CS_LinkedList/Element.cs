using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CS_LinkedList
{
    class Element
    {
        public object Data { get; set; }

        public Element next;

        public Element(object data, Element next)
        {
            Data = data;
            this.next = next;
        }
    }
}
