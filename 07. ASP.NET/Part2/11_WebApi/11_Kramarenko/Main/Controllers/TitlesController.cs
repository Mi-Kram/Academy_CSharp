using Main.Data;
using Main.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Main.Controllers
{
	public class TitlesController : Controller
	{
		IConfiguration Configuration;
		public TitlesController(IConfiguration configuration)
		{
			Configuration = configuration;
		}
		// GET: TitlesController
		public async Task<ActionResult> IndexAsync()
		{
			TitlesViewModel viewModel = new TitlesViewModel();

			using (pubsContext context = new pubsContext(Configuration))
			{
				viewModel.Titles = await context.Titles.Include(x => x.Pub).ToListAsync();
				viewModel.Publishers = await context.Publishers.ToListAsync();
			}

			return View(viewModel);
		}

		// GET: TitlesController/Create
		public async Task<ActionResult> CreateAsync()
		{
			using (pubsContext context = new pubsContext(Configuration))
			{
				ViewBag.PubId = new SelectList(await context.Publishers.ToListAsync(), nameof(Publisher.PubId), nameof(Publisher.PubName));
			}
			return View();
		}

		public async Task<ActionResult> EditAsync(string id)
		{
			if (id == null) return NotFound();
			Title title = null;
			using (pubsContext context = new pubsContext(Configuration))
			{
				ViewBag.PubId = new SelectList(await context.Publishers.ToListAsync(), nameof(Publisher.PubId), nameof(Publisher.PubName));
				title = await context.Titles.FindAsync(id);
			}
			if (title == null) return NotFound();
			return View(title);
		}
	}
}
