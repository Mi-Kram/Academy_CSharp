using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using MVC_WebAPI.Models;

namespace MVC_WebAPI.Controllers.Api
{
    public class authorsController : ApiController
    {
        private pubsEntities db = new pubsEntities();

        // GET: api/authors
        public IQueryable<author> Getauthors()
        {
            return db.authors;
        }

        [Route("api/authors/state/{state}")]
        public IQueryable<author> GetauthorsByState(string state)
        {
            //return db.authors;

            return from a in db.authors
                   where a.state == state
                   select a;
        }

        // GET: api/authors/5
        [ResponseType(typeof(author))]
        public async Task<IHttpActionResult> Getauthor(string id)
        {
            author author = await db.authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            return Ok(author);
        }

        // PUT: api/authors/5
        [HttpPut]
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Putauthor(string id, author author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != author.au_id)
            {
                return BadRequest();
            }

            db.Entry(author).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!authorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/authors
        [ResponseType(typeof(author))]
        [HttpPost]
        public async Task<IHttpActionResult> Postauthor(author author)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.authors.Add(author);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                if (authorExists(author.au_id))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = author.au_id }, author);
        }

        // DELETE: api/authors/5
        [HttpDelete]
        [ResponseType(typeof(author))]
        public async Task<IHttpActionResult> Deleteauthor(string id)
        {
            author author = await db.authors.FindAsync(id);
            if (author == null)
            {
                return NotFound();
            }

            db.authors.Remove(author);
            await db.SaveChangesAsync();

            return Ok(author);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool authorExists(string id)
        {
            return db.authors.Count(e => e.au_id == id) > 0;
        }
    }
}