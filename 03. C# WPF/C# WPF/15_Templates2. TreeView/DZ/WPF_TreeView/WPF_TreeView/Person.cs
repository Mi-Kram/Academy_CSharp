using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_TreeView
{
    public class Person
    {
        public Person(string name, string lastname, string imagePath)
        {
            FirstName = name;
            LastName = lastname;
            ImagePath = imagePath;
        }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string ImagePath { get; set; }

        public Person Parent { get; set; }

        public ObservableCollection<Person> Items { get; set; } = new ObservableCollection<Person>();
    }
}
