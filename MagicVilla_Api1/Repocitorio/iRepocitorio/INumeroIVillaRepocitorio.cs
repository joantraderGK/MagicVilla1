using MagicVilla_Api1.Modelos;

namespace MagicVilla_Api1.Repocitorio.iRepocitorio
{
    public interface INumeroIVillaRepocitorio :IRepocitorio<NumeroVilla>
    {
        Task<NumeroVilla> Actualizar(NumeroVilla entidad);
       
    }
}
