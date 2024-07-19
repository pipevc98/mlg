using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using webapi.Entities;

namespace webapi.Data
{
    [Route("api/[controller]")]
    [ApiController]
    public class TiendaController : ControllerBase
    {
        // GET: api/Tienda
        [HttpGet]
        public IActionResult GetTienda()
        {
          using(mlgContext db = new mlgContext())
            {
                var lst = db.Tienda.ToList();

                if (lst == null) return BadRequest();

                return Ok(lst);
            }
        }
    }
}
