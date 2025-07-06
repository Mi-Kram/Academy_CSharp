using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SP_Process
{
    public class ProcessInfo
    {
        public int Id { get; set; }

        public string MainModuleName { get; set; }
        
        public string MainWindowTitle { get; set; }

        public bool Responding { get; set; }

        public int ThreadsCount { get; set; }

        public int HandleCount { get; set; }

        public long UsingMemory { get; set; }
    }
}
