using MagicVilla_Api1.Modelos.Dto;

namespace MagicVilla_Api1.Datos
{
    public static class VillaStore
    {

        public static List<VillaDto> villaList = new List<VillaDto>
        {
            new VillaDto{Id=1 , Nombre="Vista a la piscina",Ocupante=3,MetroCuadrado=50},
            new VillaDto{Id=1 , Nombre="Vista a la playa",Ocupante=4,MetroCuadrado=80}
        };
    }
}
