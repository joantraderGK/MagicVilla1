using AutoMapper;
using MagicVilla_Api1.Datos;
using MagicVilla_Api1.Modelos;
using MagicVilla_Api1.Modelos.Dto;
using MagicVilla_Api1.Repocitorio.iRepocitorio;
using Microsoft.AspNetCore.Http; 
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Net;

namespace MagicVilla_Api1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NumeroVillaController : ControllerBase

    {
        private readonly ILogger<NumeroVillaController> _logger;
        private readonly IVillaRepocitorio _villaRepo;
        private readonly INumeroIVillaRepocitorio _numeroRepo;

        private readonly IMapper _mapper;
        protected ApiResponse _response;
        public NumeroVillaController(ILogger<NumeroVillaController> logger, IVillaRepocitorio villaRepo , 
                                                                           INumeroIVillaRepocitorio numeroRepo , IMapper mapper)
        {
            _logger = logger;
            _villaRepo = villaRepo;
            _numeroRepo = numeroRepo;
            _mapper = mapper;
            _response = new ();
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task <ActionResult<ApiResponse>> GetNumeroVillas()
        {

            try
            {
                _logger.LogInformation("Obtener Numero Villa");

                IEnumerable<NumeroVilla> numerovillasList = await _numeroRepo.ObtenerTodos();

                _response.Resultado = _mapper.Map<IEnumerable<VillaDto>>(numerovillasList);

                _response.StatusCode = HttpStatusCode.OK;

                return Ok(_response);
            }
            catch (Exception ex )
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() {ex.ToString()};
            }
            return _response;

        }



        [HttpGet("id:int", Name = "GetNumeroVilla")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task <ActionResult<ApiResponse>> GetNUmeroVilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    _logger.LogError("Error al traer  Numero Villa con Id " + id);
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    _response.IsExitoso=false;
                    return BadRequest();
                }
                // var villa = VillaStore.villaList.FirstOrDefault(v => v.Id == id);
                var numerovilla = await _numeroRepo.Obtener(v => v.VillaNo == id);

                if (numerovilla == null)
                {
                    _response.StatusCode=HttpStatusCode.NotFound;
                    _response.IsExitoso=false;
                    return NotFound(_response);
                }

                _response.Resultado = _mapper.Map<NumeroVillaDto>(numerovilla);
                _response.StatusCode=HttpStatusCode.OK;

                return _response ;
             
               

            }
            catch(Exception ex) 
            {
                _response.IsExitoso = false;
                _response.ErrorMessages=new List<string>() { ex.ToString() };
               
            }
            return _response;

                      
        }

        [HttpPost]
        public async Task <ActionResult<ApiResponse>> CrearNumeroVilla([FromBody] NumeroVillaCreateDto createDto)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                if (await _numeroRepo.Obtener(v => v.VillaNo == createDto.VillaNo) != null)
                {
                    ModelState.AddModelError("NombreExiste", "El Numero de Villa  ya existe!");
                    return BadRequest(ModelState);
                }
                if (await _villaRepo.Obtener(v=>v.Id==createDto.VillaId)==null)
                {
                    ModelState.AddModelError("ClaveForanea", "El Id de la Villa  no existe!");
                    return BadRequest(ModelState);
                }
                if (createDto == null)
                {
                    return BadRequest(createDto);
                }
                NumeroVilla modelo = _mapper.Map<NumeroVilla>(createDto);

                modelo.FechaCreacion=DateTime.Now;
                modelo.FechaActualizacion = DateTime.Now;
                await _numeroRepo.Crear(modelo);
                _response.Resultado=modelo;
                _response.StatusCode = HttpStatusCode.Created;


                return CreatedAtRoute("GetNumeroVilla", new { id = modelo.VillaNo }, _response);

            }
            catch(Exception ex)
            {
                _response.IsExitoso = false;
                _response.ErrorMessages = new List<string>() { ex.ToString()};

            }
            return _response;
          
        }
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteNumerovilla(int id)
        {
            try
            {
                if (id == 0)
                {
                    _response.IsExitoso = false;
                    _response.StatusCode = HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                var numerovilla = await _numeroRepo.Obtener(v => v.VillaNo == id);
                if (numerovilla != null)
                {
                    _response.IsExitoso = false;
                    _response.StatusCode = HttpStatusCode.NoContent;
                    return NotFound(_response);
                }
              await  _numeroRepo.Remover(numerovilla);
                _response.StatusCode = HttpStatusCode.NoContent;
                return Ok(_response);

            }
            catch(Exception ex   )
            {
                _response.IsExitoso = false;
                _response.ErrorMessages=new List<string>() { ex.ToString() };

            }
            return BadRequest( _response);
         
        }

        [HttpPut("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task <IActionResult> UpdateNumeroVilla(int id, [FromBody] NumeroVillaUpdateDto updateDto)
        {
            {
                if (updateDto == null || id != updateDto.VillaNo)
                {
                    _response.IsExitoso = false;
                    _response.StatusCode=HttpStatusCode.BadRequest;
                    return BadRequest(_response);
                }
                if (await _villaRepo.Obtener(v=>v.Id== updateDto.VillaId)==null)
                {
                    ModelState.AddModelError("ClaveForanea", "El Id de la Villa No existe!");
                    return BadRequest(ModelState);

                }
            

                NumeroVilla modelo = _mapper.Map<NumeroVilla>(updateDto);

               
                await _numeroRepo.Actualizar(modelo);
                _response.StatusCode = HttpStatusCode.NotFound;
              

                return Ok(_response);


            }
        }
        //[HttpPatch("{id:int}")]
        //[ProducesResponseType(StatusCodes.Status204NoContent)]
        //[ProducesResponseType(StatusCodes.Status400BadRequest)]
        //public async Task <IActionResult> UpdatePartialVilla(int id, JsonPatchDocument<VillaUpdateDto> patchDto)
        //{
        //    {
        //        if (patchDto == null || id==0)
        //        {
        //            return BadRequest();
        //        }
        //        var villa=await _villaRepo.Obtener(v => v.Id == id, tracked:false);

        //        VillaUpdateDto villaDto = _mapper.Map<VillaUpdateDto>(villa); 

         
        //        if(villa==null) return BadRequest();

        //        patchDto.ApplyTo(villaDto, ModelState);
        //        if (!ModelState.IsValid)
        //        {
        //            return BadRequest(ModelState);
        //        }

        //        Villa modelo = _mapper.Map<Villa>(villaDto);

        //      await  _villaRepo.Actualizar(modelo);
        //        _response.StatusCode = HttpStatusCode.NoContent;
        //        return Ok(_response);
        //    }
        //}
    }
}
