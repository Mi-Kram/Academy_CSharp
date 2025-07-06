using Main.Data;
using Main.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Main.TagHelpers
{
    public class BooksByStateTagHelper : TagHelper
    {
        public string RowColor { get; set; } = "#fff";
        public string AltColor { get; set; } = "#ccc";
        public string State { get; set; } = "CA";
        public int Page { get; set; } = 1;
        public int OnePageCount { get; set; } = 3;
        public List<Title> BookList { get; set; }
		public Func<string, bool, string> SortFunc { get; set; }
        public string OrderColumn { get; set; } = "Title";
        public bool OrderAsc { get; set; } = true;

		public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (Page < 1) Page = 1;
            string HTMLcontent = $"<thead><tr><th><a style='text-decoration: none; color: #000; display: block;' href={SortFunc("Title", (OrderColumn == "Title" ? !OrderAsc : true))}'>Title</a></th><th><a style='text-decoration: none; color: #000; display: block;' href={SortFunc("Price", (OrderColumn == "Price" ? !OrderAsc : true))}'>Price</a></th><th>State</th></tr></thead><tbody>";

            var books = BookList.Where(x => x.Titleauthors.Select(au => au.Au.State).Contains(State));

			switch (OrderColumn)
			{
                case "Title":
                    if (OrderAsc) books = books.OrderBy(x => x.Title1);
                    else books = books.OrderByDescending(x => x.Title1);
                    break;
                case "Price":
                    if (OrderAsc) books = books.OrderBy(x => x.Price);
                    else books = books.OrderByDescending(x => x.Price);
                    break;
                default:
                    if (OrderAsc) books = books.OrderBy(x => x.Title1);
                    else books = books.OrderByDescending(x => x.Title1);
                    break;
            }

            books = books.Skip(OnePageCount * (Page - 1)).Take(OnePageCount).ToList();

            bool even = false;
            foreach (var book in books)
            {
                HTMLcontent += @$"<tr style='background: {(even ? AltColor : RowColor)}'>
<td>{book.Title1}</td>
<td>{book.Price}</td>
<td>{State}</td>
</tr>";
                even = !even;
            }
            HTMLcontent += "</tbody>";

            output.TagName = "table";
            output.Attributes.SetAttribute("style", "width: 100%;");
            output.TagMode = TagMode.StartTagAndEndTag;
            output.Content.SetHtmlContent(HTMLcontent);
        }
    }
}
