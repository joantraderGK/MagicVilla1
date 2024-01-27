using MagicVilla_Api1.Modelos;

namespace MagicVilla_Api1.Repocitorio.iRepocitorio
{
    public interface IVillaRepocitorio :IRepocitorio<Villa>
    {
        Task<Villa> Actualizar(Villa entidad);
    }
}
