﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web; 
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Threading.Tasks;
using System.Security.Claims;
using Microsoft.AspNet.Identity;
using EventosFranciscoLuna.Models;

namespace EventosFranciscoLuna.Data
{

    public class ApplicationDbContext : IdentityDbContext<Models.ApplicationUser>
    {
        public ApplicationDbContext() : base("EventosDBConnection", throwIfV1Schema: false)
        {
        }
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Evento> EventosSet { get; set; }
        public DbSet<Reserva> ReservasSet { get; set; }
        public DbSet<Salon> SalonesSet { get; set; }
    }

}