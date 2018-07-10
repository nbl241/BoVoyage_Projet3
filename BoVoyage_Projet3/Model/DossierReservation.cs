using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BoVoyage_Projet3.Models
{
    [Table(name: "DossiersReservation")]
    public class DossierReservation : BaseModel
    {
        public string NumeroCarteBancaire { get; set; }

        public decimal PrixTotal { get; set; }

        [ForeignKey("IdClient")]
        public Voyage Voyage { get; set; }
        public int IdVoyage { get; set; }

        [ForeignKey("IdClient")]
        public Client Client { get; set; }
        public int IdClient { get; set; }

        [ForeignKey("IdParticipant")]
        public Participant Participant { get; set; }
        public int IdParticipant { get; set; }
    }
}