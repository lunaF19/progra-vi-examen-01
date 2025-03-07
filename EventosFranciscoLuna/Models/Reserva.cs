using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace EventosFranciscoLuna.Models
{
    public class Reserva
    {
        [Key]
        public int IdReserva { get; set; }

        [Required]
        [StringLength(100)]
        public string NombreCliente { get; set; }

        [Required]
        [StringLength(50)]
        public string Contacto { get; set; }
        
        // Relación con Evento
        [ForeignKey("IdEvento")]
        public int IdEvento { get; set; }
        public Evento Evento { get; set; }

    }
}