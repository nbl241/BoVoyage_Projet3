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
        /// <summary>
        /// Retourne la liste des voyages
        /// </summary>
        /// <returns></returns>
        public IQueryable<Voyage> GetVoyages()
        {
            return db.Voyages.Where(x => !x.Deleted);
        }

        // GET: api/Voyages/id
        /// <summary>
        /// Retourne un voyage selon l'id spécifié
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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

        // GET: api/Voyages/Search
        /// <summary>
        /// Permet de chercher un voyage selon le paramètre spécifié
        /// </summary>
        /// <param name="dateAller"></param>
        /// <param name="dateRetour"></param>
        /// <param name="placesDisponibles"></param>
        /// <param name="tarifToutCompris"></param>
        /// <param name="idAgenceVoyage"></param>
        /// <param name="idDestination"></param>
        /// <returns></returns>
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

        // POST: api/Voyages
        /// <summary>
        /// Permet d'ajouter un voyage
        /// </summary>
        /// <param name="voyage"></param>
        /// <returns></returns>
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

        // PUT: api/Voyages/5
        /// <summary>
        /// Permet de modifier un voyage
        /// </summary>
        /// <param name="id"></param>
        /// <param name="voyage"></param>
        /// <returns></returns>
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

        // DELETE: api/Voyages/5
        /// <summary>
        /// Permet de supprimer un voyage
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(Voyage))]
        public IHttpActionResult DeleteVoyage(int id)
        {
            Voyage voyage = db.Voyages.Find(id);
            if (voyage == null)
            {
                return NotFound();
            }

            // db.Voyages.Remove(voyage);
            voyage.Deleted = true;
            voyage.DeletedAt = DateTime.Now;
            db.Entry(voyage).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return Ok(voyage);
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