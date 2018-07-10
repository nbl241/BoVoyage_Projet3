using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using BoVoyage_Projet3.Data;
using BoVoyage_Projet3.Models;

namespace BoVoyage_Projet3.Controllers
{
    public class VoyagesController : ApiController
    {
        private BoVoyageDbContext db = new BoVoyageDbContext();

        // GET: api/Voyages
        public IQueryable<Voyage> GetVoyages()
        {
            return db.Voyages;
        }

        // GET: api/Voyages/5
        [ResponseType(typeof(Voyage))]
        public IHttpActionResult GetVoyage(int id)
        {
            Voyage voyage = db.Voyages.Find(id);
            if (voyage == null)
            {
                return NotFound();
            }

            return Ok(voyage);
        }

        // PUT: api/Voyages/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutVoyage(int id, Voyage voyage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != voyage.ID)
            {
                return BadRequest();
            }

            db.Entry(voyage).State = System.Data.Entity.EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!VoyageExists(id))
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

        // POST: api/Voyages
        [ResponseType(typeof(Voyage))]
        public IHttpActionResult PostVoyage(Voyage voyage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Voyages.Add(voyage);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = voyage.ID }, voyage);
        }

        // DELETE: api/Voyages/5
        [ResponseType(typeof(Voyage))]
        public IHttpActionResult DeleteVoyage(int id)
        {
            Voyage voyage = db.Voyages.Find(id);
            if (voyage == null)
            {
                return NotFound();
            }

            db.Voyages.Remove(voyage);
            db.SaveChanges();

            return Ok(voyage);
        }

        // GET: api/Voyages/Search
        [Route("api/Voyages/search")]
        public IQueryable<Voyage> GetSearch(DateTime? dateAller = null, DateTime? dateRetour = null, int? placesDisponibles = null, decimal? tarifToutCompris = null, int? idAgenceVoyage = null, int? idDestination = null)
        {
            var t = db.Voyages.Where(x => !x.Deleted);
            if (dateAller != null)
                t = t.Where(x => x.DateAller == dateAller);

            if (dateRetour != null)
                t = t.Where(x => x.DateRetour == dateRetour);

            if (placesDisponibles != null)
                t = t.Where(x => x.PlacesDisponibles == placesDisponibles);

            if (tarifToutCompris != null)
                t = t.Where(x => x.TarifToutCompris == tarifToutCompris);

            if (idAgenceVoyage != null)
                t = t.Where(x => x.IdAgenceVoyage == idAgenceVoyage);

            if (idDestination != null)
                t = t.Where(x => x.IdDestination == idDestination);

            return t;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool VoyageExists(int id)
        {
            return db.Voyages.Count(e => e.ID == id) > 0;
        }
    }
}