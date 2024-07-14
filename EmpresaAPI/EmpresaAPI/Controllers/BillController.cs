using EmpresaAPI.Models.TablesTbl;
using Microsoft.AspNetCore.Mvc;
using StripeTestBakery.Models.Context;

namespace EmpresaAPI.Controllers
{
    [ApiController]
    [Route("api/")]
    public class BillController : Controller
    {
        private readonly DbConnectionContext _context;

        public BillController(DbConnectionContext context)
        {
            this._context = context;
        }

        [HttpGet]
        [Route("Bills")]
        public IActionResult GetBills()
        {
            try
            {
                var prodts = _context.BillTbl.Select(p => new
                {
                    p.BillId,
                    p.ClientId,
                    p.ProductId,
                    p.BillAmount,
                    p.BillQuantity,
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
        [Route("Bill/{id}")]
        public IActionResult GetBill(int id)
        {
            try
            {
                var prodt = _context.BillTbl.Where(p => p.BillId == id).Select(p => new
                {
                    p.BillId,
                    p.ClientId,
                    p.ProductId,
                    p.BillAmount,
                    p.BillQuantity,
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
        [Route("Bill")]
        public IActionResult PostBill([FromBody] BillTbl prodt)
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
                    _context.BillTbl.Add(prodt);
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
        [Route("Bill/{id}")]
        public IActionResult PutBill(int id, [FromBody] BillTbl prodt)
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
                var prodtext = _context.BillTbl.Find(id);
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
                    prodtext.ClientId = prodt.ClientId;
                    prodtext.ProductId = prodt.ProductId;
                    prodtext.BillAmount = prodt.BillAmount;
                    prodtext.BillQuantity = prodt.BillQuantity;
                    prodtext.ModifiedDateTime = DateTime.UtcNow;
                    _context.BillTbl.Update(prodtext);
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
        [Route("Bill/{id}")]
        public IActionResult DeltBill(int id)
        {
            try
            {
                var prodt = _context.BillTbl.Find(id);
                if (prodt == null)
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Billo no encontrado.",
                        error = prodt
                    });
                }
                else
                {

                    _context.BillTbl.Remove(prodt);
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
