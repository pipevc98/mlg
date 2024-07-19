using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using webapi.Entities.response;
using webapi.Entities;
using webapi.Entities.Request;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using webapi.Business.auth;

namespace webapi.Data
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class ArticulosController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            Respuesta oRespuesta = new Respuesta();
            oRespuesta.Exito = 0;

            try
            {

                using (mlgContext db = new mlgContext())
                {
                    var lst = db.Articulos.ToList();
                    oRespuesta.Exito = 1;
                    oRespuesta.Data = lst;

                }
            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }

            return Ok(oRespuesta);

        }

        [HttpPost]
        public IActionResult Post(ArticuloRequest oModel)
        {
            Respuesta oRespuesta = new Respuesta();
            oRespuesta.Exito = 0;
            try
            {
                using(mlgContext db = new mlgContext())
                {
                    Articulo oArticulo = new Articulo();
                    oArticulo.Codigo = oModel.codigo;
                    oArticulo.Descripcion = oModel.descripcion;
                    oArticulo.Stock = oModel.stock;
                    oArticulo.Nombre = oModel.nombre;
                    oArticulo.Imagen = oModel.imagen;
                    oArticulo.Precio = oModel.precio;
                    db.Articulos.Add(oArticulo);
                    db.SaveChanges();
                    oRespuesta.Exito = 1;
                }
            }
            catch(Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }

            return Ok(oRespuesta);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(long id)
        {
            if(id== 0)
            {
                return BadRequest("veriffica el id");
            } 
            try
            {
                using (mlgContext db = new mlgContext())
                {
                    Articulo articulo = db.Articulos.Find(id)!;

                    db.Remove(articulo);
                    db.SaveChanges();

                    
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);  
            }

            return Ok();

        }
    }
}
