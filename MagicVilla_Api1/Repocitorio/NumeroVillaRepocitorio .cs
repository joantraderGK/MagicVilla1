using MagicVilla_Api1.Datos;
using MagicVilla_Api1.Modelos;
using MagicVilla_Api1.Repocitorio.iRepocitorio;

namespace MagicVilla_Api1.Repocitorio
{
    public class NumeroVillaRepocitorio : Repocitorio<NumeroVilla>, INumeroIVillaRepocitorio
    {
        private readonly ApplicationDbContext _db;

        public NumeroVillaRepocitorio(ApplicationDbContext db) :base(db) 
        {
            _db = db;
        }
        public async Task<NumeroVilla> Actualizar(NumeroVilla entidad)
        {
            entidad.FechaActualizacion = DateTime.Now;
            _db.numerovillas.Update(entidad);
            await _db.SaveChangesAsync();
            return entidad;
        }
    }
}
