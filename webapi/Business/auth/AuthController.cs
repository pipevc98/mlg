
using Microsoft.AspNetCore.Mvc;

using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using webapi.Entities;
using webapi.Entities.Request;

namespace webapi.Business.auth
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {

        public IConfiguration? _configuration;

        public AuthController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [HttpPost("login")]
        public dynamic Login([FromBody] object login)
        {
            if (login == null) return  BadRequest();
            var data = JsonConvert.DeserializeObject<dynamic>(login.ToString());
            using ( mlgContext db = new mlgContext())
            {
                string user = data!.username.ToString();
                string password = data.password.ToString();

                Cliente cliente = db.Clientes.FirstOrDefault(x => x.User == user && x.Password == password)!;


                if (cliente == null )
                {
                    return NotFound(new { message = "usuario o contraseña incorrectos" });
                   
                }

                var jwt = _configuration!.GetSection("jwt").Get<Jwt>();

                var claims = new[]
                {
                    new Claim(JwtRegisteredClaimNames.Sub, jwt!.Subject!),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat, DateTime.UtcNow.ToString()),
                    new Claim("nombre", cliente.Nombre!),
                    new Claim("apellido", cliente.Apellido!),
                    new Claim("direccion", cliente.Direccion!),
                    new Claim("user", cliente.User!),
                    new Claim("id", cliente.Id.ToString())
                };

                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt.Key!));
                var singin = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                var token = new JwtSecurityToken(
                        jwt.Issuer,
                        jwt.Audience,
                        claims,
                        expires: DateTime.Now.AddMinutes(60),
                        signingCredentials: singin
                    );

                return new
                {
                    success = true,
                    Message = "Success",
                    result = new JwtSecurityTokenHandler().WriteToken(token),
                    data = cliente
                };
            }
        }

        



    }
    
}
