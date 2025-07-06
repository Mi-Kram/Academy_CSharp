using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Main.Models
{
	public class ServerTitle
	{
        public string TitleId { get; set; }
        public string Title1 { get; set; }
        public string Type { get; set; }
        public string PubId { get; set; }
        public string Price { get; set; }
        public string Advance { get; set; }
        public string Royalty { get; set; }
        public string YtdSales { get; set; }
        public string Notes { get; set; }
        public string Pubdate { get; set; }

        public TitleFormModel GetFormModel()
		{
            return new TitleFormModel(this);
		}
    }
}
