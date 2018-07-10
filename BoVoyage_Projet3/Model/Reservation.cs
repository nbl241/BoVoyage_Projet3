using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoVoyage_Projet3.Models
{
    public class Reservation : BaseModel
    {
        public string NumeroCarteBancaire { get; set; }

        public decimal PrixTotal { get; set; }

        public int IdVoyage { get; set; }

        public int IdClient { get; set; }

        public int IdParticipant { get; set; }
    }
}