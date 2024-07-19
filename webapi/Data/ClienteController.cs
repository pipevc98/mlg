using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using webapi.Entities;
using webapi.Entities.response;
using webapi.Entities.Request;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;

namespace webapi.Data
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClienteController : ControllerBase
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
                    var lst = db.Clientes.ToList();
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
        public async Task<IActionResult> Add([FromBody] ClienteRequest oModel)
        {
            Respuesta oRespuesta = new Respuesta();
            oRespuesta.Exito = 0;
            try
            {
                using( mlgContext db = new mlgContext())
                {
                    var user = oModel.User;

                    //Check Username exist 
                    if ( await CheckUserExist(user!))
                    {
                        return BadRequest(new { Message = "El usuario ya existe" });
                    }

                    Cliente oCliente = new Cliente();
                    oCliente.Nombre = oModel.Nombre;
                    oCliente.Apellido = oModel.Apellido;
                    oCliente.User = oModel.User;
                    oCliente.Password = oModel.Password;
                    oCliente.Direccion = oModel.Direccion;
                    await db.Clientes.AddAsync(oCliente);
                    await db.SaveChangesAsync();
                    oRespuesta.Exito = 1;
                }
            }
            catch(Exception ex)
            {
                return Ok(new
                {
                    Message = ex.Message
                });
            }

            return Ok(new
            {
                Message = "cliente registrado"
            });
        }

        [HttpPut]
        public IActionResult Update(ClienteRequest oModel)
        {
            Respuesta oRespuesta = new Respuesta();
            oRespuesta.Exito = 0;
            try
            {
                using(mlgContext db = new mlgContext())
                {
                    Cliente? oCliente = db.Clientes.Find(oModel.Id)!;
                    oCliente.Nombre = oModel.Nombre;
                    oCliente.Apellido = oModel.Apellido;
                    oCliente.User = oModel.User;
                    oCliente.Password = oModel.Password;
                    oCliente.Direccion = oModel.Direccion;
                    db.Entry(oCliente).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
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

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            Respuesta oRespuesta = new Respuesta();
            oRespuesta.Exito = 0;
            try
            {
                using (mlgContext db = new mlgContext())
                {
                    Cliente? oCliente = db.Clientes.Find(id)!;

                    db.Remove(oCliente);
                    db.SaveChanges();
                    oRespuesta.Exito = 1;
                }

            }
            catch (Exception ex)
            {
                oRespuesta.Mensaje = ex.Message;
            }
            return Ok(oRespuesta);
        }

        

        private async Task<bool> CheckUserExist(string user)
        {
            using (mlgContext db = new mlgContext())
            {
                return await db.Clientes.AnyAsync(x => x.User == user);
            }
        }

    }
}
