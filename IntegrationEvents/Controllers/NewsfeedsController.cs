using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;
using System.Web.Http.Description;
using IntegrationEvents.Models;
using IntegrationEvents.Models.Entities;

namespace IntegrationEvents.Controllers
{
    public class NewsfeedsController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Newsfeeds
        public IQueryable<Newsfeed> GetNewsfeeds()
        {
            return db.Newsfeeds.Include(n => n.Items);
        }

        // GET: api/Newsfeeds/5
        [ResponseType(typeof(Newsfeed))]
        public IHttpActionResult GetNewsfeed(int id)
        {
            Newsfeed newsfeed = db.Newsfeeds.Find(id);
            if (newsfeed == null)
            {
                return NotFound();
            }

            return Ok(newsfeed);
        }

        // PUT: api/Newsfeeds/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutNewsfeed(int id, Newsfeed newsfeed)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != newsfeed.Id)
            {
                return BadRequest();
            }

            db.Entry(newsfeed).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NewsfeedExists(id))
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

        // POST: api/Newsfeeds
        [ResponseType(typeof(Newsfeed))]
        public IHttpActionResult PostNewsfeed(Newsfeed newsfeed)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Newsfeeds.Add(newsfeed);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = newsfeed.Id }, newsfeed);
        }

        // DELETE: api/Newsfeeds/5
        [ResponseType(typeof(Newsfeed))]
        public IHttpActionResult DeleteNewsfeed(int id)
        {
            Newsfeed newsfeed = db.Newsfeeds.Find(id);
            if (newsfeed == null)
            {
                return NotFound();
            }

            db.Newsfeeds.Remove(newsfeed);
            db.SaveChanges();

            return Ok(newsfeed);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool NewsfeedExists(int id)
        {
            return db.Newsfeeds.Count(e => e.Id == id) > 0;
        }
    }
}