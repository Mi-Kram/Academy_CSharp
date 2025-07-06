using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Properties
{
	class Student
	{
		private string name;

		private int age;
		private int height;

		// классическое свойство, предоставляет доступ к полю
		public int Age
		{
			get
			{
				return age;
			}
			set
			{
				if (value >= 0)
				{
					age = value;
				}
			}
		}

		// свойство, использующее lambda-выражения
		public int Height
		{
			get => height;
		}

		// автосвойство, поле класса создаётся неявно
		public int Weight { get; set; }

		public Student(string name, int age, int height, int weight)
		{
			this.name = name;
			if (age >= 0)
				this.age = age;
			else
				this.age = 10;

			if (height >= 38)
				this.height = height;
			else
				this.height = 160;

			if (weight >= 40)
				this.Weight = weight;
			else
				this.Weight = 40;
		}

		public void Print()
		{
			Console.WriteLine($"Name: {name}, Age: {age}, Height: {height}, Wieght: {Weight}");
		}
	}
}
