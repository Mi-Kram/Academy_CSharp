using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Models
{
	public class TitleItem : INotifyPropertyChanged
	{
		private string _id;
		private string _pubName;
		private double? _price;
		private string _title;

		public string Title
		{
			get { return _title; }
			set { _title = value; OnProppertyChanged(nameof(Title)); }
		}

		public double? Price
		{
			get { return _price; }
			set { _price = value; OnProppertyChanged(nameof(Price)); }
		}

		public string ID
		{
			get { return _id; }
			set { _id = value; OnProppertyChanged(nameof(ID)); }
		}

		public string PubName
		{
			get { return _pubName; }
			set { _pubName = value; OnProppertyChanged(nameof(PubName)); }
		}

		public event PropertyChangedEventHandler PropertyChanged;
		private void OnProppertyChanged(string prop = "")
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}
	}
}
