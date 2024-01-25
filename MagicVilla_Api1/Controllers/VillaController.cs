using AutoMapper;
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
        private readonly IMapper _mapper;
        public VillaController(ILogger<VillaController> logger, ApplicationDbContext db , IMapper mapper)
        {
            _logger = logger;
            _db = db;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task <ActionResult<IEnumerable<VillaDto>>> GetVillas()
        {
            _logger.LogInformation("Obtener las Villa");
            IEnumerable<Villa> villasList =  await _db.villas.ToListAsync();
            return Ok( _mapper.Map<IEnumerable<VillaDto>>(villasList ));

        }



        [HttpGet("id:int", Name = "GetVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task <ActionResult<VillaDto>> GetVilla(int id)
        {
            if (id == 0)
            {
                _logger.LogError("Error al traer Villa con Id " + id);
                return BadRequest();
            }
           // var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
           var villa = await _db.villas.FirstOrDefaultAsync(v => v.Id == id);

            if (villa == null)
            {
                return BadRequest();
            }

            return Ok(_mapper.Map<VillaDto>(villa));
        }

        [HttpPost]
        public async Task <ActionResult<VillaDto>> CrearVilla([FromBody] VillaCreateDto createDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            if (await  _db.villas.FirstOrDefaultAsync(v => v.Nombre.ToLower() == createDto.Nombre.ToLower()) != null)
            {
                ModelState.AddModelError("NombreExiste", "La Villa con ese Nombre ya existe!");
                return BadRequest(ModelState);
            }
            if (createDto == null)
            {
                return BadRequest(createDto);
            }

            Villa modelo = _mapper.Map<Villa>(createDto);
       
          
           await _db.villas.AddAsync(modelo);
            await _db.SaveChangesAsync();

            return CreatedAtRoute("Get", new { id = modelo.Id }, modelo);
        }
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task< IActionResult> Delete(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var villa = await _db.villas.FirstOrDefaultAsync(v => v.Id == id);
            if (villa != null)
            {
                return NotFound();
            }
          _db.villas.Remove(villa);
          await  _db.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task <IActionResult> UpdateVilla(int id, [FromBody] VillaUpdateDto updateDto)
        {
            {
                if (updateDto == null || id != updateDto.Id)
                {
                    return BadRequest();
                }
                //var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
                //villa.Nombre = villaDto.Nombre;
                //villa.Ocupante = villaDto.Ocupante;
                //villa.MetroCuadrado = villaDto.MetroCuadrado;

                Villa modelo = _mapper.Map<Villa>(updateDto);

               
                _db.villas.Update(modelo);
              await _db.SaveChangesAsync();

                return NoContent();


            }
        }
        [HttpPatch("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task <IActionResult> UpdatePartialVilla(int id, JsonPatchDocument<VillaUpdateDto> patchDto)
        {
            {
                if (patchDto == null || id==0)
                {
                    return BadRequest();
                }
                var villa=await _db.villas.AsNoTracking().FirstOrDefaultAsync(v => v.Id == id);

                VillaUpdateDto villaDto = _mapper.Map<VillaUpdateDto>(villa); 

         
                if(villa==null) return BadRequest();

                patchDto.ApplyTo(villaDto, ModelState);
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                Villa modelo = _mapper.Map<Villa>(villaDto);

          

                _db.villas.Update(modelo);
                await _db.SaveChangesAsync();
                return NoContent();
            }
        }
    }
}
