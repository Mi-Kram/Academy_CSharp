using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main
{
    class PageInfo
    {
        public string Path { get; set; }
        public bool IsEdit { get; set; }

        public PageInfo(string path = "", bool isEdit = false)
        {
            Path = path;
            IsEdit = isEdit;
        }
    }
}
