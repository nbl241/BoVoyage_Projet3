using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BoVoyage_Projet3.Models
{
    [Table(name: "Voyages")]
    public class Voyage : BaseModel
    {
        public DateTime DateAller { get; set; }

        public DateTime DateRetour { get; set; }

        public int PlacesDisponibles { get; set; }

        public decimal TarifToutCompris { get; set; }

        [ForeignKey("IdAgenceVoyage")]
        public AgenceVoyage AgenceVoyage { get; set; }
        public int IdAgenceVoyage { get; set; }

        [ForeignKey("IdDestination")]
        public Destination Destination { get; set; }
        public int IdDestination { get; set; }
    }
}