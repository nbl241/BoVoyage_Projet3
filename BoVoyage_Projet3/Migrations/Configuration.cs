using BoVoyage_Projet3.Data;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace BoVoyage_Projet3.Migration
{
    public class Configuration : DbMigrationsConfiguration<BoVoyageDbContext>
    {
        //création d'un constructeur
        public Configuration()
        {
            //migration non automatique
            AutomaticMigrationsEnabled = false;
        }
    }
}