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
    public class ParticipantsController : ApiController
    {
        private BoVoyageDbContext db = new BoVoyageDbContext();

        // GET: api/Participants
        /// <summary>
        /// Retourne la liste des participants
        /// </summary>
        /// <returns></returns>
        public IQueryable<Participant> GetParticipants()
        {
            return db.Participants.Where(x => !x.Deleted);
        }

        // GET: api/Participants/id
        /// <summary>
        /// Retourne un participant selon l'id spécifié
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(Participant))]
        public IHttpActionResult GetParticipant(int id)
        {
            Participant participant = db.Participants.Find(id);
            if (participant == null)
            {
                return NotFound();
            }

            return Ok(participant);
        }

        // GET: api/Participants/Search
        /// <summary>
        /// Permet de chercher un participant selon le paramètre spécifié
        /// </summary>
        /// <param name="nom"></param>
        /// <param name="prenom"></param>
        /// <param name="telephone"></param>
        /// <param name="dateNaissance"></param>
        /// <param name="age"></param>
        /// <returns></returns>
        [Route("api/Participants/search")]
        public IQueryable<Participant> GetSearch(string nom = "", string prenom = "", string telephone = "", DateTime? dateNaissance = null, int? age = null)
        {
            var t = db.Participants.Where(x => !x.Deleted);
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

            return t;
        }

        // POST: api/Participants
        /// <summary>
        /// Permet d'ajouter un participant
        /// </summary>
        /// <param name="participant"></param>
        /// <returns></returns>
        [ResponseType(typeof(Participant))]
        public IHttpActionResult PostParticipant(Participant participant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            // Validations 
            if (participant.Nom.Trim() == "") return BadRequest();


            db.Participants.Add(participant);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = participant.ID }, participant);
        }

        // PUT: api/Participants/5
        /// <summary>
        /// Permet de modifier un participant
        /// </summary>
        /// <param name="id"></param>
        /// <param name="participant"></param>
        /// <returns></returns>
        [ResponseType(typeof(void))]
        public IHttpActionResult PutParticipant(int id, Participant participant)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != participant.ID)
            {
                return BadRequest();
            }

            db.Entry(participant).State = System.Data.Entity.EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ParticipantExists(id))
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

        // DELETE: api/Participants/5
        /// <summary>
        /// Permet de supprimer un participant
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(Participant))]
        public IHttpActionResult DeleteParticipant(int id)
        {
            Participant participant = db.Participants.Find(id);
            if (participant == null)
            {
                return NotFound();
            }

            // db.Participants.Remove(participant);
            participant.Deleted = true;
            participant.DeletedAt = DateTime.Now;
            db.Entry(participant).State = System.Data.Entity.EntityState.Modified;
            db.SaveChanges();

            return Ok(participant);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ParticipantExists(int id)
        {
            return db.Participants.Count(e => e.ID == id) > 0;
        }
    }
}