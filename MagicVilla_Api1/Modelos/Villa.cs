﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MagicVilla_Api1.Modelos
{
    public class Villa
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Nombre { get; set; }

        public string Detalle { get; set; }
        [Required]
        public int Tarifa { get; set; }

        public int Ocupante {  get; set; }

        public double MetroCuadrado { get; set; }

        public string ImagenUr {  get; set; }

        public string Amenidad { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime FechaActulizacion { get; set; }
    }
}
