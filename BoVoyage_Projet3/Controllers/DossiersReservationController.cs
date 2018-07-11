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
        /// <summary>
        /// Retourne la liste des dossiers de reservation
        /// </summary>
        /// <returns></returns>
        public IQueryable<DossierReservation> GetDossiersReservation()
        {
            return db.DossiersReservation.Where(x => !x.Deleted);
        }

        // GET: api/DossiersReservation/id
        /// <summary>
        /// Retourne un dossier de reservation selon l'id spécifié
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Permet de chercher un dossier de reservation selon le paramètre spécifié
        /// </summary>
        /// <param name="prixTotal"></param>
        /// <param name="idVoyage"></param>
        /// <param name="idClient"></param>
        /// <param name="idParticipant"></param>
        /// <returns></returns>
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

        // POST: api/DossiersReservation
        /// <summary>
        /// Permet d'ajouter un dossier de reservation
        /// </summary>
        /// <param name="dossierReservation"></param>
        /// <returns></returns>
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

        // PUT: api/DossiersReservation/5
        /// <summary>
        /// Permet de modifier un dossier de reservation
        /// </summary>
        /// <param name="id"></param>
        /// <param name="dossierReservation"></param>
        /// <returns></returns>
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

        // DELETE: api/DossiersReservation/5
        /// <summary>
        /// Permet de supprimer un dossier de reservation
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(DossierReservation))]
        public IHttpActionResult DeleteDossierReservation(int id)
        {
            DossierReservation dossierReservation = db.DossiersReservation.Find(id);
            if (dossierReservation == null)
            {
                return NotFound();
            }

            // db.DossiersReservation.Remove(dossierReservation);
            dossierReservation.Deleted = true;
            dossierReservation.DeletedAt = DateTime.Now;
            db.Entry(dossierReservation).State = System.Data.Entity.EntityState.Modified;
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