using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Main.Data;
using Main.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace Main.Pages.Pictures
{
    public class DetailPictureModel : PageModel
    {
        private readonly ApplicationDBContext _context;
        private readonly IWebHostEnvironment _hostingEnvironment;

		public Picture Picture { get; set; }

		public DetailPictureModel(ApplicationDBContext context, IWebHostEnvironment hostingEnvironment)
        {
            _context = context;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (!id.HasValue) return RedirectToPage("/Pictures");

            Picture = await _context.Pictures
                .Where(x => x.ID == id)
                .Include(x => x.Category)
                .Include(x => x.Comments)
                .Include(x => x.User)
                .FirstOrDefaultAsync();

			Picture.Comments = Picture.Comments.OrderByDescending(x => x.Date).ToList();

			if (Picture == null) return RedirectToPage("/Pictures");

            return Page();
        }

		public async Task<JsonResult> OnPostAddCommentAsync(int? pictureID, string msg)
		{
			if (!pictureID.HasValue || msg == null) return new JsonResult(null);
			msg = msg.Trim(' ', '\t', '\n');
			if (msg.Length == 0) return new JsonResult(null);

			var pictures = _context.Pictures.Where(x => x.ID == pictureID);
			if (pictures.Count() != 1) return new JsonResult(null);

			try
			{
				string myID = await GetMyID();
				Picture picture = pictures.First();
				Comment comment = new Comment()
				{
					Date = DateTime.Now,
					Content = msg,
					PictureID = pictureID.Value,
					UserID = myID
				};
				_context.Comments.Add(comment);
				_context.SaveChanges();

				msg = msg.Replace("\n", "<br>");
				return new JsonResult(new
				{
					isOk = true,
					comment = new
					{
						date = comment.Date.ToString(@"dd.MM.yyyy HH:mm"),
						msg = msg
					}
				});
			}
			catch { }

			return new JsonResult(false);
		}

		private async Task<string> GetMyID()
		{
			if (!User.Identity.IsAuthenticated) return "";
			return (await _context.Users.Where(x => x.UserName == User.Identity.Name).FirstOrDefaultAsync())?.Id ?? "";
		}

	}
}
