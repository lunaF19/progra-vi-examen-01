using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema; 

namespace EventosFranciscoLuna.Models
{
    [Table("Salones")]
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