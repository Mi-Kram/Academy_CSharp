using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Models
{
	public class ServerPublisher : INotifyPropertyChanged
	{
		private string _pubId;

		public string PubId
		{
			get { return _pubId; }
			set { _pubId = value; OnPropertyChanged(nameof(PubId)); }
		}

		//public string PubId { get; set; }
        public string PubName { get; set; }
        public string City { get; set; }
        public string State { get; set; }

		public event PropertyChangedEventHandler PropertyChanged;
		private void OnPropertyChanged(string prop)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}
	}
}
