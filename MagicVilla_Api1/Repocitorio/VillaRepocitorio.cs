using MagicVilla_Api1.Datos;
using MagicVilla_Api1.Modelos;
using MagicVilla_Api1.Repocitorio.iRepocitorio;

namespace MagicVilla_Api1.Repocitorio
{
    public class VillaRepocitorio : Repocitorio<Villa>, IVillaRepocitorio
    {
        private readonly ApplicationDbContext _db;

        public VillaRepocitorio(ApplicationDbContext db) :base(db) 
        {
            _db = db;
        }
        public async Task<Villa> Actualizar(Villa entidad)
        {
            entidad.FechaActulizacion=DateTime.Now;
            _db.villas.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;
        }
    }
}
