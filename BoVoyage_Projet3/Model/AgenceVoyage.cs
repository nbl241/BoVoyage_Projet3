using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BoVoyage_Projet3.Models
{
    [Table(name: "AgencesVoyage")]
    public class AgenceVoyage : BaseModel
    {
        public string Name { get; set; }
    }
}