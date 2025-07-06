using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace NP_WCF_Stream_Service
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service1" in both code and config file together.
    public class Service1 : IService1
    {
        Stream st;

        public Stream GetFile()
        {
            st = new FileStream(@"c:\temp\video.mp4", FileMode.Open, FileAccess.Read);
            return st;
        }

        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public byte[] GetArray()
        {
            FileStream st = new FileStream(@"c:\temp\video.mp4", FileMode.Open, FileAccess.Read);

            byte[] arr = new byte[st.Length];
            st.Read(arr, 0, (int)st.Length);

            st.Close();

            return arr;
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }
}
