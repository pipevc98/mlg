using Microsoft.EntityFrameworkCore.Storage.ValueConversion.Internal;

namespace webapi.Entities
{
    public class register
    {
        public string nombre { get; set; }
        public string apellido { get; set; }
        public string direccion { get; set; }
        public string password { get; set; }
        public string user { get; set; }
    }
}
