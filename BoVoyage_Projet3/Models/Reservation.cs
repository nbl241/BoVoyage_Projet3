using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoVoyageTest.Models
{
    public class Reservation : BaseModel
    {
        public int Numerunique { get; set; }

        public string NumeroCarteBancaire { get; set; }

        public decimal PrixTotal { get; set; }

        public int IdVoyage { get; set; }

        public int IdClient { get; set; }

        public int IdParticipant { get; set; }
    }
}