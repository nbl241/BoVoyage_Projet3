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
    public class ClientsController : ApiController
    {
        private BoVoyageDbContext db = new BoVoyageDbContext();

        // GET: api/Clients
        /// <summary>
        /// Retourne la liste des clients
        /// </summary>
        /// <returns></returns>
        public IQueryable<Client> GetClients()
        {
            return db.Clients;
        }

        // GET: api/Clients/id
        /// <summary>
        /// Retourne la liste des clients selon l'id spécifié
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(Client))]
        public IHttpActionResult GetClient(int id)
        {
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return NotFound();
            }

            return Ok(client);
        }

        // GET: api/Clients/Search
        /// <summary>
        /// Permet de chercher un client selon le paramètre spécifié
        /// </summary>
        /// <param name="nom"></param>
        /// <param name="prenom"></param>
        /// <param name="telephone"></param>
        /// <param name="dateNaissance"></param>
        /// <param name="age"></param>
        /// <param name="email"></param>
        /// <returns></returns>
        [Route("api/Clients/search")]
        public IQueryable<Client> GetSearch(string nom = "", string prenom = "", string telephone = "", DateTime? dateNaissance = null, int? age = null, string email = "")
        {
            var t = db.Clients.Where(x => !x.Deleted);
            if (!string.IsNullOrWhiteSpace(nom))
                t = t.Where(x => x.Nom.Contains(nom));

            if (!string.IsNullOrWhiteSpace(prenom))
                t = t.Where(x => x.Prenom.Contains(prenom));

            if (!string.IsNullOrWhiteSpace(telephone))
                t = t.Where(x => x.Telephone.Contains(telephone));

            if (dateNaissance != null)
                t = t.Where(x => x.DateNaissance == dateNaissance);

            if (age != null)
                t = t.Where(x => x.Age == age);

            if (!string.IsNullOrWhiteSpace(email))
                t = t.Where(x => x.Email.Contains(email));

            return t;
        }

        // POST: api/Clients
        /// <summary>
        /// Permet d'ajouter un client
        /// </summary>
        /// <param name="client"></param>
        /// <returns></returns>
        [ResponseType(typeof(Client))]
        public IHttpActionResult PostClient(Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Clients.Add(client);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = client.ID }, client);
        }

        // PUT: api/Clients/5
        /// <summary>
        /// Permet de modifier un client
        /// </summary>
        /// <param name="id"></param>
        /// <param name="client"></param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult PutClient(int id, Client client)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != client.ID)
            {
                return BadRequest();
            }

            db.Entry(client).State = System.Data.Entity.EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ClientExists(id))
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

        // DELETE: api/Clients/5
        /// <summary>
        /// Permet de supprimer un client
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(Client))]
        public IHttpActionResult DeleteClient(int id)
        {
            Client client = db.Clients.Find(id);
            if (client == null)
            {
                return NotFound();
            }

            db.Clients.Remove(client);
            db.SaveChanges();

            return Ok(client);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ClientExists(int id)
        {
            return db.Clients.Count(e => e.ID == id) > 0;
        }
    }
}