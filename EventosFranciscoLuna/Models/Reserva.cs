using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations; 

namespace EventosFranciscoLuna.Models
{
    [Table("Reservas")]
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
        [ForeignKey("Evento")]
        public int IdEvento { get; set; }
        public Evento Evento { get; set; }

    }
}