using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BoVoyageTest.Models
{
    public class Voyage : BaseModel
    {
        public DateTime DateAller { get; set; }

        public DateTime DateRetour { get; set; }

        public int PlacesDisponibles { get; set; }

        public decimal TarifToutCompris { get; set; }

        public int IdAgenceVoyage { get; set; }

        public int IdDestination { get; set; }
    }
}