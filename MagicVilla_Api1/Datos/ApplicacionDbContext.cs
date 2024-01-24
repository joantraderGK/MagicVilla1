using MagicVilla_Api1.Modelos;
using Microsoft.EntityFrameworkCore;

namespace MagicVilla_Api1.Datos
{
    public class ApplicationDbContext :DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base (options) 
        { 
        
        }

        public DbSet<Villa> villas { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Villa>().HasData(
                new Villa()
                {
                    Id = 1,
                    Nombre="Villa Real",
                    Detalle="Detalle de la Villa...",
                    ImagenUr="",
                    Ocupante=5,
                    MetroCuadrado=50,
                    Tarifa=200,
                    Amenidad="",
                    FechaCreacion=DateTime.Now,
                    FechaActulizacion=DateTime.Now,


                });
            new Villa()
            {
                Id = 2,
                Nombre = "Primium Vista a la P",
                Detalle = "Detalle de la Villa...",
                ImagenUr = "",
                Ocupante = 4,
                MetroCuadrado = 40,
                Tarifa = 150,
                Amenidad = "",
                FechaCreacion = DateTime.Now,
                FechaActulizacion = DateTime.Now,


            };












        }




    }
}
