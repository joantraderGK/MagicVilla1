using System.ComponentModel.DataAnnotations;

namespace MagicVilla_Api1.Modelos.Dto
{
    public class VillaUpdateDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Nombre { get; set; }

        public string Detalle { get; set; }
        [Required]
        public int Tarifa { get; set; }
        [Required]
        public int Ocupante { get; set; }
        [Required]
        public double MetroCuadrado { get; set; }
        [Required]
        public string ImagenUr { get; set; }

        public string Amenidad { get; set; }

      
    }
}
