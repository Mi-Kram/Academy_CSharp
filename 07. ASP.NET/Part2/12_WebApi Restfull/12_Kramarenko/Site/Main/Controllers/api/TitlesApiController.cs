using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Main.Data;
using Main.Models;
using Microsoft.AspNetCore.Authorization;

namespace Main.Controllers.api
{
    //[AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class TitlesApiController : ControllerBase
    {
        private readonly pubsContext _context;

        public TitlesApiController(pubsContext context)
        {
            _context = context;
        }

        // GET: api/TitlesApi
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Title>>> GetTitles()
        {
            return await _context.Titles.ToListAsync();
        }

        // GET: api/TitlesApi/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Title>> GetTitle(string id)
        {
            var title = await _context.Titles.FindAsync(id);

            if (title == null)
            {
                return NotFound();
            }

            return title;
        }

        [HttpGet("{pubId}/{page}")]
        public async Task<JsonResult> GetTitlesByPage(string pubId, int? page)
		{
            if (pubId == null || pubId.Length == 0 || page == null) 
                return new JsonResult(null);

            List<Title> lst = null;
            if (pubId == "0") lst = await _context.Titles.Include(x => x.Pub).ToListAsync();
            else lst = await _context.Titles.Where(x => x.PubId == pubId).Include(x => x.Pub).ToListAsync();

            int cnt = lst.Count / 5;
            if (lst.Count % 5 != 0) cnt++;

            return new JsonResult(new
            {
                titles = lst.Skip((page.Value - 1) * 5).Take(5).Select(x => new
                {
                    id = x.TitleId,
                    title = x.Title1,
                    price = x.Price.HasValue ? string.Format("{0:0.##}", x.Price.Value) : "",
                    pubName = x.Pub.PubName
                }),
                paggingCnt = cnt
            });
        }

        [HttpGet("publishers")]
        public async Task<JsonResult> GetPublishers()
		{
            return new JsonResult(await _context.Publishers.ToListAsync());
        }

        [Route("pub/{pubId}")]
        public async Task<IActionResult> TitlesByPubAsync(string pubId)
        {
            if (pubId == null || pubId.Length == 0) return new JsonResult(null);
            List<Title> lst = null;
            
            if (pubId == "0") lst = await _context.Titles
                    .Include(x => x.Pub).ToListAsync();
            else lst = await _context.Titles
                .Where(x => x.PubId == pubId)
                .Include(x => x.Pub).ToListAsync();

            int cnt = lst.Count / 5;
            if (lst.Count % 5 != 0) cnt++;

            return new JsonResult(new
            {
                titles = lst.Take(5).Select(x => new
                {
                    id = x.TitleId,
                    title = x.Title1,
                    price = x.Price.HasValue ? string.Format("{0:0.##}", x.Price.Value) : "",
                    pubName = x.Pub.PubName
                }),
                paggingCnt = cnt
            });
        }


		// PUT: api/TitlesApi/5
		// To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
		[HttpPut("{id}")]
        public async Task<IActionResult> PutTitle(string id, Title title)
        {
            if (id != title.TitleId)
            {
                return BadRequest();
            }

            List<(string key, string value)> errors = new List<(string key, string value)>();
            foreach (var item in ModelState)
            {
                if (item.Value.Errors.Count == 0) continue;
                errors.Add((item.Key, item.Value.Errors[0].ErrorMessage));
            }
            if (errors.Count > 0) return new JsonResult(new
            {
                hasErrors = true,
                errors = errors.Select(x => new
                {
                    key = x.key,
                    value = x.value
                })
            });

            _context.Entry(title).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return new JsonResult(new
                {
                    isSuccess = true,
                    url = Url.Action("Index", "Titles")
                });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TitleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
        }

        [HttpPost]
        public async Task<ActionResult<Title>> PostTitle(TitleModel title)
        {
            List<(string key, string value)> errors = title.GetErrors();
            if (errors.Count > 0) return new JsonResult(new
            {
                hasErrors = true,
                errors = errors.Select(x => new
                {
                    key = x.key,
                    value = x.value
                })
            });

            string id = "";
            do
            {
                id = Guid.NewGuid().ToString().Substring(0, 6);
            } while (_context.Titles.Any(x => x.PubId == id));

            title.TitleId = id;
            _context.Titles.Add(title.GetTitle());

            try
            {
                await _context.SaveChangesAsync();
                return new JsonResult(new
                {
                    isSuccess = true,
                    url = Url.Action("Index", "Titles")
                });
            }
            catch (DbUpdateException)
            {
                if (TitleExists(title.TitleId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }
        }


        // DELETE: api/TitlesApi/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTitle(string id)
        {
            var title = await _context.Titles.FindAsync(id);
            if (title == null)
            {
                return NotFound();
            }

            _context.Titles.Remove(title);
            await _context.SaveChangesAsync();

            return new JsonResult(id);
        }

        private bool TitleExists(string id)
        {
            return _context.Titles.Any(e => e.TitleId == id);
        }
    }
}
