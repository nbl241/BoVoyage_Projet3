﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BoVoyage_Projet3.Models
{
    public abstract class BaseModel
    {
        //[key]
        public int ID { get; set; }
        
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        
        public DateTime CreatedAt { get; set; }

        public bool Deleted { get; set; }

        //pour rendre une valeur nullable en base de donnée sur le type primitif
        public DateTime? DeletedAt { get; set; }
    }
}