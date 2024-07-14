using EmpresaAPI.Models.TablesTbl;
using Microsoft.AspNetCore.Mvc;
using StripeTestBakery.Models.Context;

namespace EmpresaAPI.Controllers
{
    [ApiController]
    [Route("api/")]
    public class PhotoController : Controller
    {
        private readonly DbConnectionContext _context;

        public PhotoController(DbConnectionContext context)
        {
            this._context = context;
        }

        [HttpGet]
        [Route("Photos")]
        public IActionResult GetPhotos()
        {
            try
            {
                var prodts = _context.PhotoTbl.Select(p => new
                {
                    p.PhotoId,
                    p.PhotoName,
                    p.PhotoDescription,
                    p.PhotoCategory,
                    p.PhotoUrl,
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
        [Route("Photo/{id}")]
        public IActionResult GetPhoto(int id)
        {
            try
            {
                var prodt = _context.PhotoTbl.Where(p => p.PhotoId == id).Select(p => new
                {
                    p.PhotoId,
                    p.PhotoName,
                    p.PhotoDescription,
                    p.PhotoCategory,
                    p.PhotoUrl,
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
        [Route("Photo")]
        public IActionResult PostPhoto([FromBody] PhotoTbl prodt)
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
                    _context.PhotoTbl.Add(prodt);
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
        [Route("Photo/{id}")]
        public IActionResult PutPhoto(int id, [FromBody] PhotoTbl prodt)
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
                var prodtext = _context.PhotoTbl.Find(id);
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
                    prodtext.PhotoName = prodt.PhotoName;
                    prodtext.PhotoDescription = prodt.PhotoDescription;
                    prodtext.PhotoCategory = prodt.PhotoCategory;
                    prodtext.PhotoUrl = prodt.PhotoUrl;
                    prodtext.ModifiedDateTime = DateTime.UtcNow;
                    _context.PhotoTbl.Update(prodtext);
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
        [Route("Photo/{id}")]
        public IActionResult DeltPhoto(int id)
        {
            try
            {
                var prodt = _context.PhotoTbl.Find(id);
                if (prodt == null)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Photoo no encontrado.",
                        error = prodt
                    });
                }
                else
                {

                    _context.PhotoTbl.Remove(prodt);
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
