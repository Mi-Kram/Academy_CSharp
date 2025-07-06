using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Main.Models
{
	public class TitleModel
	{
        public TitleModel() { }
		public TitleModel(Title title)
		{
            TitleId = title.TitleId;
            Title1 = title.Title1;
            Type = title.Type;
            PubId = title.PubId;
            Price = title.Price.HasValue ? title.Price.ToString() : "";
            Advance = title.Advance.HasValue ? title.Advance.ToString() : "";
            Royalty = title.Royalty.HasValue ? title.Royalty.ToString() : "";
            YtdSales = title.YtdSales.HasValue ? title.YtdSales.ToString() : "";
            Notes = title.Notes;
            Pubdate = title.Pubdate.ToString(@"yyyy-MM-dd");
		}

        public Title GetTitle()
		{
            bool isPrice = decimal.TryParse(Price, out decimal price);
            bool isAdvance = decimal.TryParse(Advance, out decimal advance);
            bool isRoyalty = int.TryParse(Royalty, out int royalty);
            bool isYtdSales = int.TryParse(YtdSales, out int ytdSales);
            bool isPubdate = DateTime.TryParseExact(Pubdate, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out DateTime pubdate);
            if(!isPubdate) isPubdate = DateTime.TryParse(Pubdate, out pubdate);

            return new Title()
            {
                TitleId = TitleId,
                Title1 = Title1,
                Price = isPrice ? price : null,
                Royalty = isRoyalty ? royalty : null,
                Advance = isAdvance ? advance : null,
                YtdSales = isYtdSales ? ytdSales : null,
                PubId = PubId,
                Notes = Notes,
                Type = Type,
                Pubdate = isPubdate ? pubdate : new DateTime(1,1,1)
            };
		}

        public List<(string key, string value)> GetErrors()
		{
            List<(string key, string value)> result = new List<(string key, string value)>();

            if (Title1.Length == 0) result.Add((nameof(Title1), "This field is required!"));
            if (Type.Length == 0) result.Add((nameof(Type), "This field is required!"));
            if (PubId.Length == 0) result.Add((nameof(PubId), "This field is required!"));
            if (Pubdate.Length == 0) result.Add((nameof(Pubdate), "This field is required!"));

            return result;
		}

        public string TitleId { get; set; }
        [Required]
        [StringLength(80)]
        [Display(Name = "Title")]
        public string Title1 { get; set; }
        [Required]
        [StringLength(12)]
        public string Type { get; set; }
        [Required]
        [StringLength(4)]
        [Display(Name = "Publisher")]
        public string PubId { get; set; }
        [Required]
        public string Price { get; set; }
        [Required]
        public string Advance { get; set; }
        [Required]
        public string Royalty { get; set; }
        [Required]
        public string YtdSales { get; set; }
        [StringLength(200)]
        public string Notes { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public string Pubdate { get; set; }
    }
}
