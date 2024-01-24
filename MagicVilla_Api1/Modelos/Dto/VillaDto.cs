﻿using System.ComponentModel.DataAnnotations;

namespace MagicVilla_Api1.Modelos.Dto
{
    public class VillaDto
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Nombre { get; set; }

        public string Detalle { get; set; }
        [Required]
        public int Tarifa { get; set; }

        public int Ocupante { get; set; }

        public double MetroCuadrado { get; set; }

        public string ImagenUr { get; set; }

        public string Amenidad { get; set; }

      
    }
}
