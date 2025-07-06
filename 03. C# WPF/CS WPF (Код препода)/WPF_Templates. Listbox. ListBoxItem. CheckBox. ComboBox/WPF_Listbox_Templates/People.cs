using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Advanced_Templates
{
    public class People: List<Person>
    {
        public People()
        {
            Add(new Person() { Id = 1, FirstName = "Ivan", LastName = "Petrov", Address = "Donetsk", Age = 23 });
            Add(new Person() { Id = 1, FirstName = "Vasya", LastName = "Pupkin", Address = "Rostov", Age = 34 });
            Add(new Person() { Id = 1, FirstName = "Anna", LastName = "Antonova", Address = "Piter", Age = 12 });
            Add(new Person() { Id = 1, FirstName = "Alex", LastName = "Lozovoy", Address = "Donetsk", Age = 22 });
            Add(new Person() { Id = 1, FirstName = "Elena", LastName = "Basova", Address = "Donetsk", Age = 35 });
        }
    }
}
