using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace EventosFranciscoLuna.Models
{
    public class Salon
    {
        [Key]
        public int IdSalon { get; set; }

        [Required]
        [StringLength(100)]
        public string Nombre { get; set; }

        [Required]
        public int Capacidad { get; set; }

        [Required]
        [StringLength(200)]
        public string Ubicacion { get; set; }

    }
}