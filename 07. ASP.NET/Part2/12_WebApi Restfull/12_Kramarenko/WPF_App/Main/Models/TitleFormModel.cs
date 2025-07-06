using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Models
{
	public class TitleFormModel : INotifyPropertyChanged
	{
		private string _id = "";
		private string _title = "";
		private string _type = "";
		private string _pubId = "";
		private string _price = "";
		private string _advance = "";
		private string _royalty = "";
		private string _ytdSales = "";
		private string _notes = "";
		private string _pubdate = "";

		public string ID
		{
			get { return _id; }
			set { _id = value; }
		}
		public string Title
		{
			get { return _title; }
			set { _title = value; }
		}
		public string Type
		{
			get { return _type; }
			set { _type = value; }
		}
		public string PubId
		{
			get { return _pubId; }
			set { _pubId = value; }
		}
		public string Price
		{
			get { return _price; }
			set { _price = value; }
		}
		public string Advance
		{
			get { return _advance; }
			set { _advance = value; }
		}
		public string Royalty
		{
			get { return _royalty; }
			set { _royalty = value; }
		}
		public string YtdSales
		{
			get { return _ytdSales; }
			set { _ytdSales = value; }
		}
		public string Notes
		{
			get { return _notes; }
			set { _notes = value; }
		}
		public string Pubdate
		{
			get { return _pubdate; }
			set { _pubdate = value; }
		}

		public event PropertyChangedEventHandler PropertyChanged;

		public void OnPropertyChanged(string prop)
		{
			PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
		}

		public TitleFormModel() { }
		public TitleFormModel(ServerTitle title)
		{
			_id = title.TitleId;
			_title = title.Title1;

			_type = title.Type;
			_pubId = title.PubId;
			_price = title.Price;
			_advance = title.Advance;
			_royalty = title.Royalty;
			_ytdSales = title.YtdSales;
			_notes = title.Notes;
			_pubdate = title.Pubdate;
		}

		public ServerTitle GetTitle()
		{
			return new ServerTitle()
			{
				TitleId = _id,
				Title1 = _title,
				Type = _type,
				PubId = _pubId,
				Price = _price,
				Advance = _advance,
				Royalty = _royalty,
				YtdSales = _ytdSales,
				Notes = _notes,
				Pubdate = _pubdate
			};
		}
	}
}
