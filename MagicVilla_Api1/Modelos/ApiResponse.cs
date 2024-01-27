using System.Net;

namespace MagicVilla_Api1.Modelos
{
    public class ApiResponse
    {
        public HttpStatusCode StatusCode { set; get; }

        public bool IsExitoso { set; get; } = true;

        public List<string> ErrorMessages { set; get; }

        public object Resultado { set; get; }
    }
}
