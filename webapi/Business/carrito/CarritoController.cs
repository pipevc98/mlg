using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.Entities;

namespace webapi.Business.carrito
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CarritoController : ControllerBase
    {
        [HttpPost("{id}")]
        public async Task<IActionResult> agregarArticulo(int id, [FromBody] int articuloId )
        {
            

            using (mlgContext db = new mlgContext())
            {


                Cliente cliente = await db.Clientes.Include(c=> c.Articulo ).FirstOrDefaultAsync(x => x.Id == id);
                if (cliente == null) return NotFound("Cliente no encontrado");
                Articulo articulo = await db.Articulos.FirstOrDefaultAsync(x => x.Id == articuloId);
                if (articulo == null) return NotFound("Artículo no encontrado");

                cliente.Articulo.Add(articulo);

                articulo.ClienteId = id;

                await db.SaveChangesAsync();

                return Ok(cliente);

            }
        }

        [HttpGet("{id}")]
        public IActionResult getUser(int id)
            {
                using (mlgContext db = new mlgContext())
                {
                    Cliente cliente = db.Clientes.Include(c => c.Articulo).FirstOrDefault(x => x.Id == id);
                    return Ok(cliente);
                }
            }

        [HttpDelete("{id}/{articuloId}")]
        public async Task<IActionResult> DeleteArticulos( int id, int articuloId)
        {
           using(mlgContext db = new mlgContext())
            {
                Console.WriteLine(id);
                Console.WriteLine(articuloId);
                Articulo articulo = await db.Articulos.FirstOrDefaultAsync( x => x.Id == articuloId && x.ClienteId == id);
                Console.WriteLine(articulo);

                if (articulo == null) return NotFound();

                articulo.ClienteId = null;
                await db.SaveChangesAsync();

                return Ok(articulo);
            }
        }
    }
}
