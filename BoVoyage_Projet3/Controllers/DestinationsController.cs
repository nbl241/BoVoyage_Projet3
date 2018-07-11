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
    public class DestinationsController : ApiController
    {
        private BoVoyageDbContext db = new BoVoyageDbContext();

        // GET: api/Destinations
        /// <summary>
        /// Retourne la liste des destinations
        /// </summary>
        /// <returns></returns>
        public IQueryable<Destination> GetDestinations()
        {
            return db.Destinations.Where(x => !x.Deleted);
        }

        // GET: api/Destinations/id
        /// <summary>
        /// Retourne une destination selon l'id spécifié
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(Destination))]
        public IHttpActionResult GetDestination(int id)
        {
            Destination destination = db.Destinations.Find(id);
            if (destination == null)
            {
                return NotFound();
            }

            return Ok(destination);
        }

        // GET: api/Destinations/Search
        /// <summary>
        /// Permet de chercher une destination selon le paramètre spécifié
        /// </summary>
        /// <param name="continent"></param>
        /// <param name="pays"></param>
        /// <param name="region"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        [Route("api/Destinations/search")]
        public IQueryable<Destination> GetSearch(string continent = "", string pays = "", string region = "", string description = "")
        {
            var t = db.Destinations.Where(x => !x.Deleted);
            if (!string.IsNullOrWhiteSpace(continent))
                t = t.Where(x => x.Continent.Contains(continent));

            if (!string.IsNullOrWhiteSpace(pays))
                t = t.Where(x => x.Pays.Contains(pays));

            if (!string.IsNullOrWhiteSpace(region))
                t = t.Where(x => x.Region.Contains(region));

            if (!string.IsNullOrWhiteSpace(description))
                t = t.Where(x => x.Description.Contains(description));

            return t;
        }

        // POST: api/Destinations
        /// <summary>
        /// Permet d'ajouter une destination
        /// </summary>
        /// <param name="destination"></param>
        /// <returns></returns>
        [ResponseType(typeof(Destination))]
        public IHttpActionResult PostDestination(Destination destination)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Destinations.Add(destination);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = destination.ID }, destination);
        }

        // PUT: api/Destinations/5
        /// <summary>
        /// Permet de modifier une destination
        /// </summary>
        /// <param name="id"></param>
        /// <param name="destination"></param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult PutDestination(int id, Destination destination)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != destination.ID)
            {
                return BadRequest();
            }

            db.Entry(destination).State = System.Data.Entity.EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DestinationExists(id))
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

        // DELETE: api/Destinations/5
        /// <summary>
        /// Permet de supprimer une destination
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(Destination))]
        public IHttpActionResult DeleteDestination(int id)
        {
            Destination destination = db.Destinations.Find(id);
            if (destination == null)
            {
                return NotFound();
            }

            // db.Destinations.Remove(destination);
            destination.Deleted = true;
            destination.DeletedAt = DateTime.Now;
            db.Entry(destination).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return Ok(destination);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DestinationExists(int id)
        {
            return db.Destinations.Count(e => e.ID == id) > 0;
        }
    }
}