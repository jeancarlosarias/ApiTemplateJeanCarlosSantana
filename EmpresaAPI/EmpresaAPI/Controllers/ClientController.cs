using EmpresaAPI.Models.TablesTbl;
using Microsoft.AspNetCore.Mvc;
using StripeTestBakery.Models.Context;

namespace EmpresaAPI.Controllers
{
    [ApiController]
    [Route("api/")]
    public class ClientController : Controller
    {
        private readonly DbConnectionContext _context;

        public ClientController(DbConnectionContext context)
        {
            this._context = context;
        }

        [HttpGet]
        [Route("Clients")]
        public IActionResult GetClients()
        {
            try
            {
                var prodts = _context.ClientTbl.Select(p => new
                {
                    p.ClientId,
                    p.ClientName,
                    p.ClientDirection,
                    p.ClientTel,
                    p.ModifiedDateTime
                }).ToList();

                if (prodts.Count == 0)
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "No hay ningun registro.",
                        error = prodts
                    });
                }
                else
                {
                    return Ok(new
                    {
                        success = true,
                        message = "Todos los registros han sido cargados exitosamente.",
                        result = prodts
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Internal Server Error",
                    error = ex.Message
                });
            }
        }

        [HttpGet]
        [Route("Client/{id}")]
        public IActionResult GetClient(int id)
        {
            try
            {
                var prodt = _context.ClientTbl.Where(p => p.ClientId == id).Select(p => new
                {
                    p.ClientId,
                    p.ClientName,
                    p.ClientDirection,
                    p.ClientTel,
                    p.ModifiedDateTime
                }).FirstOrDefault();
                if (prodt == null)
                {
                    return NotFound(new
                    {
                        success = false,
                        message = "No hay ningun registro.",
                        error = prodt
                    });
                }
                else
                {
                    return Ok(new
                    {
                        success = true,
                        message = $"El registro de ID: {id}, han sido cargados exitosamente.",
                        result = prodt
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Internal Server Error",
                    error = ex.Message
                });
            }
        }

        [HttpPost]
        [Route("Client")]
        public IActionResult PostClient([FromBody] ClientTbl prodt)
        {
            if (!ModelState.IsValid)
            {

                return BadRequest(new
                {
                    success = false,
                    message = "Estructura de modelo incorrecta.",
                    error = prodt
                });
            }
            try
            {
                if (prodt == null)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Faltan parametros.",
                        error = prodt
                    });
                }
                else
                {
                    _context.ClientTbl.Add(prodt);
                    _context.SaveChanges();
                    return Ok(new
                    {
                        success = true,
                        message = "El registro han sido guardado exitosamente.",
                        result = prodt
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Internal Server Error",
                    error = ex.Message
                });
            }
        }

        [HttpPut]
        [Route("Client/{id}")]
        public IActionResult PutClient(int id, [FromBody] ClientTbl prodt)
        {
            if (!ModelState.IsValid)
            {

                return BadRequest(new
                {
                    success = false,
                    message = "Estructura de modelo incorrecta.",
                    error = prodt
                });
            }
            try
            {
                var prodtext = _context.ClientTbl.Find(id);
                if (prodt == null || prodtext == null)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Faltan parametros o producto no encontrado.",
                        error = prodt
                    });
                }
                else
                {
                    prodtext.ClientName = prodt.ClientName;
                    prodtext.ClientDirection = prodt.ClientDirection;
                    prodtext.ClientName = prodt.ClientName;
                    prodtext.ClientTel = prodt.ClientTel;
                    prodtext.ModifiedDateTime = DateTime.UtcNow;
                    _context.ClientTbl.Update(prodtext);
                    _context.SaveChanges();
                    return Ok(new
                    {
                        success = true,
                        message = $"El registro con ID:{id}, han sido actualizado exitosamente.",
                        result = prodtext
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Internal Server Error",
                    error = ex.Message
                });
            }
        }

        [HttpDelete]
        [Route("Client/{id}")]
        public IActionResult DeltClient(int id)
        {
            try
            {
                var prodt = _context.ClientTbl.Find(id);
                if (prodt == null)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Cliento no encontrado.",
                        error = prodt
                    });
                }
                else
                {

                    _context.ClientTbl.Remove(prodt);
                    _context.SaveChanges();
                    return Ok(new
                    {
                        success = true,
                        message = $"El registro con ID:{id}, han sido eliminado exitosamente.",
                        result = prodt
                    });
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    success = false,
                    message = "Internal Server Error",
                    error = ex.Message
                });
            }
        }
    }
}
