using System.Security.Claims;
using webapi.Entities;

namespace webapi.Business.auth
{
    public class Jwt
    {
        public string? Key { get; set; }
        public string? Issuer { get; set; }
        public string? Audience { get; set; }
        public string? Subject { get; set; } 

        public static dynamic ValidarToken(ClaimsIdentity identity)
        {   
            using( mlgContext db = new mlgContext())
            {

                try
                {
                    if (identity.Claims.Count() == 0)
                    {
                        return new
                        {
                            success = false,
                            message = "verificar si existe un token",
                            result = ""
                        };
                    }

                    var id = identity.Claims.FirstOrDefault(x => x.Type == "Id").ToString();
                    Cliente cliente = db.Clientes.FirstOrDefault(x => x.Id.ToString() == id)!;
                    return new
                    {
                        success = true,
                        message = "Exito",
                        Results = cliente
                    };
                }
                catch (Exception ex)
                {
                    return new
                    {
                        success = false,
                        message = ex.Message,
                        result = ""
                    };
                }

            }   

        }

    }
}
