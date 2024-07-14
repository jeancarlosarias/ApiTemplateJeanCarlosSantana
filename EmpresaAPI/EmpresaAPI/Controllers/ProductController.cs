using EmpresaAPI.Models.TablesTbl;
using Microsoft.AspNetCore.Mvc;
using StripeTestBakery.Models.Context;

namespace EmpresaAPI.Controllers
{
    [ApiController]
    [Route("api/")]
    public class ProductController : Controller
    {
        private readonly DbConnectionContext _context;

        public ProductController(DbConnectionContext context)
        {
            this._context = context;
        }

        [HttpGet]
        [Route("Products")]
        public IActionResult GetProducts() 
        {
            try
            {
                var prodts = _context.ProductTbl.Select(p => new
                {
                    p.ProductId,
                    p.PhotoId,
                    p.ProductName,
                    p.ProductDescription,
                    p.ProductPrice,
                    p.ProductQuantity,
                    p.ModifiedDateTime
                }).ToList();

                if(prodts.Count == 0)
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
        [Route("Product/{id}")]
        public IActionResult GetProduct(int id)
        {
            try
            {
                var prodt = _context.ProductTbl.Where(p => p.ProductId == id).Select(p => new
                {
                    p.ProductId,
                    p.PhotoId,
                    p.ProductName,
                    p.ProductDescription,
                    p.ProductPrice,
                    p.ProductQuantity,
                    p.ModifiedDateTime
                }).FirstOrDefault();
                if(prodt == null)
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
        [Route("Product")]
        public IActionResult PostProduct([FromBody] ProductTbl prodt)
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
                if(prodt == null)
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
                    _context.ProductTbl.Add(prodt);
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
        [Route("Product/{id}")]
        public IActionResult PutProduct(int id, [FromBody] ProductTbl prodt) 
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
                var prodtext = _context.ProductTbl.Find(id);
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
                    prodtext.PhotoId = prodt.PhotoId;
                    prodtext.ProductName = prodt.ProductName;
                    prodtext.ProductDescription = prodt.ProductDescription;
                    prodtext.ProductPrice = prodt.ProductPrice;
                    prodtext.ProductQuantity = prodt.ProductQuantity;
                    prodtext.ModifiedDateTime = DateTime.UtcNow;
                    _context.ProductTbl.Update(prodtext);
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
        [Route("Product/{id}")]
        public IActionResult DeltProduct(int id)
        {
            try
            {
                var prodt = _context.ProductTbl.Find(id);
                if (prodt == null )
                {
                    return BadRequest(new
                    {
                        success = false,
                        message = "Producto no encontrado.",
                        error = prodt
                    });
                }
                else
                {
                    
                    _context.ProductTbl.Remove(prodt);
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
