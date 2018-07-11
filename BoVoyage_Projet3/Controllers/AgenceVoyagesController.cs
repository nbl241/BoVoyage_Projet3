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
    public class AgenceVoyagesController : ApiController
    {
        private BoVoyageDbContext db = new BoVoyageDbContext();

        // GET: api/AgenceVoyages
        /// <summary>
        /// Retourne la liste des agences
        /// </summary>
        /// <returns></returns>
        public IQueryable<AgenceVoyage> GetAgencesVoyage()
        {
            return db.AgencesVoyage.Where(x => !x.Deleted);
        }

        // GET: api/AgenceVoyages/id
        /// <summary>
        /// Retourne le nom d'une agence selon l'id spécifié
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(AgenceVoyage))]
        public IHttpActionResult GetAgenceVoyage(int id)
        {
            AgenceVoyage agenceVoyage = db.AgencesVoyage.Find(id);
            if (agenceVoyage == null)
            {
                return NotFound();
            }

            return Ok(agenceVoyage);
        }

        //GET: api/AgenceVoyages/search
        /// <summary>
        /// Permet de chercher une agence selon le nom spécifié
        /// </summary>
        /// <param name="nom"></param>
        /// <returns></returns>
        [Route("api/AgenceVoyages/search")]
        public IQueryable<AgenceVoyage> GetSearch(string nom = "")
        {
            var t = db.AgencesVoyage.Where(x => !x.Deleted);
            if (!string.IsNullOrWhiteSpace(nom))
                t = t.Where(x => x.Nom.Contains(nom));

            return t;
        }

        // POST: api/AgenceVoyages
        /// <summary>
        /// Permet d'ajouter une agence
        /// </summary>
        /// <param name="agenceVoyage"></param>
        /// <returns></returns>
        [ResponseType(typeof(AgenceVoyage))]
        public IHttpActionResult PostAgenceVoyage(AgenceVoyage agenceVoyage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validations 
            if (agenceVoyage.Nom.Trim() == "") return BadRequest();

            db.AgencesVoyage.Add(agenceVoyage);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = agenceVoyage.ID }, agenceVoyage);
        }

        // PUT: api/AgenceVoyages/5
        /// <summary>
        /// Permet de modifier une agence
        /// </summary>
        /// <param name="id"></param>
        /// <param name="agenceVoyage"></param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult PutAgenceVoyage(int id, AgenceVoyage agenceVoyage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != agenceVoyage.ID)
            {
                return BadRequest();
            }

            db.Entry(agenceVoyage).State = System.Data.Entity.EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AgenceVoyageExists(id))
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

        // DELETE: api/AgenceVoyages/5
        /// <summary>
        /// Permet de supprimer une agence
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(AgenceVoyage))]
        public IHttpActionResult DeleteAgenceVoyage(int id)
        {
            AgenceVoyage agenceVoyage = db.AgencesVoyage.Find(id);
            if (agenceVoyage == null)
            {
                return NotFound();
            }

            // db.AgencesVoyage.Remove(agenceVoyage);
            agenceVoyage.Deleted = true;
            agenceVoyage.DeletedAt = DateTime.Now;
            db.Entry(agenceVoyage).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return Ok(agenceVoyage);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AgenceVoyageExists(int id)
        {
            return db.AgencesVoyage.Count(e => e.ID == id) > 0;
        }
    }
}