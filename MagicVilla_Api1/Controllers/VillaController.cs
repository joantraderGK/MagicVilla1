using MagicVilla_Api1.Datos;
using MagicVilla_Api1.Modelos;
using MagicVilla_Api1.Modelos.Dto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace MagicVilla_Api1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VillaController : ControllerBase

    {
        private readonly ILogger<VillaController> _logger;
        private readonly ApplicationDbContext _db;

        public VillaController(ILogger<VillaController> logger, ApplicationDbContext db)
        {
            _logger = logger;
            _db = db;
        }

        [HttpGet]
        public ActionResult<IEnumerable<VillaDto>> GetVillas()
        {
            _logger.LogInformation("Obtener las Villa");
            return Ok(_db.villas.ToList());

        }



        [HttpGet("id:int", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<VillaDto> GetVilla(int id)
        {
            if (id == 0)
            {
                _logger.LogError("Error al traer Villa con Id " + id);
                return BadRequest();
            }
           // var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
           var villa = _db.villas.FirstOrDefault(v => v.Id == id);

            if (villa == null)
            {
                return BadRequest();
            }

            return Ok(villa);
        }

        [HttpPost]
        public ActionResult<VillaDto> CrearVilla([FromBody] VillaDto villaDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (_db.villas.FirstOrDefault(v => v.Nombre.ToLower() == villaDto.Nombre.ToLower()) != null)
            {
                ModelState.AddModelError("NombreExiste", "La Villa con ese Nombre ya existe!");
                return BadRequest(ModelState);
            }
            if (villaDto == null)
            {
                return BadRequest();
            }
            if (villaDto.Id == 0)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);

            }
            Villa modelo = new()
            {
                Nombre = villaDto.Nombre,
                Detalle = villaDto.Detalle,
                ImagenUr = villaDto.ImagenUr,
                Ocupante = villaDto.Ocupante,
                Tarifa = villaDto.Tarifa,
                MetroCuadrado = villaDto.MetroCuadrado,
                Amenidad = villaDto.Amenidad,

            };
            _db.villas.Add(modelo);
            _db.SaveChanges();

            return CreatedAtRoute("Get", new { id = villaDto.Id }, villaDto);
        }
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = _db.villas.FirstOrDefault(v => v.Id == id);
            if (villa != null)
            {
                return NotFound();
            }
          _db.villas.Remove(villa);
            _db.SaveChanges();
            return NoContent();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateVilla(int id, [FromBody] VillaDto villaDto)
        {
            {
                if (villaDto == null || id != villaDto.Id)
                {
                    return BadRequest();
                }
                //var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
                //villa.Nombre = villaDto.Nombre;
                //villa.Ocupante = villaDto.Ocupante;
                //villa.MetroCuadrado = villaDto.MetroCuadrado;

                Villa modelo = new()
                {
                    Id = villaDto.Id,
                    Nombre = villaDto.Nombre,
                    Detalle = villaDto.Detalle,
                    ImagenUr = villaDto.ImagenUr,
                    Ocupante = villaDto.Ocupante,
                    Tarifa = villaDto.Tarifa,
                    MetroCuadrado = villaDto.MetroCuadrado, 
                    Amenidad = villaDto.Amenidad,

                };
                _db.villas.Update(modelo);
                _db.SaveChanges();

                return NoContent();


            }
        }
        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdatePartialVilla(int id, JsonPatchDocument<VillaDto> patchDto)
        {
            {
                if (patchDto == null || id==0)
                {
                    return BadRequest();
                }
                var villa=_db.villas.AsNoTracking().FirstOrDefault(v => v.Id == id);

                VillaDto villaDto = new VillaDto()
                {
                    Id=villa.Id,
                    Nombre=villa.Nombre,
                    Detalle=villa.Detalle,
                    ImagenUr=villa.ImagenUr,
                    Ocupante=villa.Ocupante,
                    Tarifa=villa.Tarifa,
                    MetroCuadrado=villa.MetroCuadrado,
                    Amenidad=villa.Amenidad,
                };
                if(villa==null) return BadRequest();

                patchDto.ApplyTo(villaDto, ModelState);
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                Villa modelo = new()
                {
                    Id = villaDto.Id,
                    Nombre = villaDto.Nombre,
                    Detalle = villaDto.Detalle,
                    ImagenUr = villaDto.ImagenUr,
                    Ocupante = villaDto.Ocupante,
                    Tarifa = villaDto.Tarifa,
                    MetroCuadrado = villaDto.MetroCuadrado,
                    Amenidad = villaDto.Amenidad,

                };

                _db.villas.Update(modelo);
                _db.SaveChanges();
                return NoContent();
            }
        }
    }
}
