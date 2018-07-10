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
    public class DossiersReservationController : ApiController
    {
        private BoVoyageDbContext db = new BoVoyageDbContext();

        // GET: api/DossiersReservation
        public IQueryable<DossierReservation> GetDossiersReservation()
        {
            return db.DossiersReservation;
        }

        // GET: api/DossiersReservation/5
        [ResponseType(typeof(DossierReservation))]
        public IHttpActionResult GetDossierReservation(int id)
        {
            DossierReservation dossierReservation = db.DossiersReservation.Find(id);
            if (dossierReservation == null)
            {
                return NotFound();
            }

            return Ok(dossierReservation);
        }

        // GET: api/DossiersReservation/Search
        [Route("api/DossiersReservation/search")]
        public IQueryable<DossierReservation> GetSearch(decimal? prixTotal = null, int? idVoyage = null, int? idClient = null, int? idParticipant = null)
        {
            var t = db.DossiersReservation.Where(x => !x.Deleted);
            if (prixTotal != null)
                t = t.Where(x => x.PrixTotal == prixTotal);

            if (idVoyage != null)
                t = t.Where(x => x.IdVoyage == idVoyage);

            if (idClient != null)
                t = t.Where(x => x.IdClient == idClient);

            if (idParticipant != null)
                t = t.Where(x => x.IdParticipant == idParticipant);

            return t;
        }

        // PUT: api/DossiersReservation/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDossierReservation(int id, DossierReservation dossierReservation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != dossierReservation.ID)
            {
                return BadRequest();
            }

            db.Entry(dossierReservation).State = System.Data.Entity.EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DossierReservationExists(id))
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

        // POST: api/DossiersReservation
        [ResponseType(typeof(DossierReservation))]
        public IHttpActionResult PostDossierReservation(DossierReservation dossierReservation)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.DossiersReservation.Add(dossierReservation);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = dossierReservation.ID }, dossierReservation);
        }

        // DELETE: api/DossiersReservation/5
        [ResponseType(typeof(DossierReservation))]
        public IHttpActionResult DeleteDossierReservation(int id)
        {
            DossierReservation dossierReservation = db.DossiersReservation.Find(id);
            if (dossierReservation == null)
            {
                return NotFound();
            }

            db.DossiersReservation.Remove(dossierReservation);
            db.SaveChanges();

            return Ok(dossierReservation);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DossierReservationExists(int id)
        {
            return db.DossiersReservation.Count(e => e.ID == id) > 0;
        }
    }
}