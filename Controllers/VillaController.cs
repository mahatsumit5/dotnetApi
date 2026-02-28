using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoyalVilla_API.Database;
using RoyalVilla_API.dtos;
using RoyalVilla_API.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace RoyalVilla_API.Controllers
{
    [ApiController]
    [Route("api/villa")]
    public class VillaController(ApplicationDbContext db, IMapper mapper) : ControllerBase
    {

        private readonly ApplicationDbContext _db = db;
        readonly IMapper _mapper = mapper;


        [HttpGet]
        public async Task<ActionResult<ApiResponse<List<VillaDTO>>>> GetVillas()
        {
            var villas = await _db.Villa.ToListAsync();
            List<VillaDTO> villastoFR = _mapper.Map<List<VillaDTO>>(villas);
            return ApiResponse<List<VillaDTO>>.SendSuccessResponse(villastoFR, "Villas retrived successfully");
        }



        [HttpGet("{id:int}")]
        //FromRoute is the default
        public async Task<ActionResult<ApiResponse<VillaDTO>>> GetVillaById([FromRoute] int id)
        {
            try
            {
                if (id <= 0)
                {
                    return ApiResponse<VillaDTO>.SendErrorResponse(400, "Bad Request", "Villa Id must be greater than 0");
                }
                var villa = await _db.Villa.FirstOrDefaultAsync(u => u.Id == id);
                if (villa == null)
                {
                    return ApiResponse<VillaDTO>.SendErrorResponse(404, "Not Found", $"villa with id {id} not found");
                }

                var data = _mapper.Map<VillaDTO>(villa);
                return ApiResponse<VillaDTO>.SendSuccessResponse(data, "Villa retrived successfully");
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }


        [HttpPost]

        public async Task<ActionResult<ApiResponse<VillaDTO>>> CreateVilla(VillaCreateDTO createVillaParams)
        {
            try
            {
                if (createVillaParams == null)
                {
                    return ApiResponse<VillaDTO>.SendErrorResponse(400, "Bad Request", "Villa is required");
                }
                Villa villa = _mapper.Map<Villa>(createVillaParams);
                var newVilla = await _db.Villa.AddAsync(villa);
                var result = await _db.SaveChangesAsync();

                var dataToReturn = _mapper.Map<VillaDTO>(newVilla.Entity);
                return ApiResponse<VillaDTO>.SendSuccessResponse(dataToReturn, "Villa retrived successfully");
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"error occured  error:{ex.Message}");
            }
        }

        [HttpPut("{id:int}")]
        public async Task<ActionResult<Villa>> UpdateVilla([FromRoute] int id, UpdateVillaDTO villaDTO)
        {

            try
            {
                if (villaDTO == null)
                {
                    return BadRequest("Update parameters are required");
                }

                var villaToUpdate = await _db.Villa.FirstOrDefaultAsync(u => u.Id == id);
                if (villaToUpdate == null)
                {
                    return NotFound("Villa not found");
                }
                _mapper.Map(villaDTO, villaToUpdate);

                villaToUpdate.UpdatedDate = DateTime.Now;
                await _db.SaveChangesAsync();
                return Ok(villaDTO);

            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, $"error occured  error:{ex.Message}");

            }
        }

        [HttpDelete("{id:int}")]

        public async Task<ActionResult<Villa>> DeleteVillaById([FromRoute] int id)
        {
            try
            {
                var deleteVilla = await _db.Villa.FirstOrDefaultAsync(u => u.Id == id);

                if (deleteVilla == null)
                {
                    return NotFound($"Villa not found with id {id}");
                }
                _db.Villa.Remove(deleteVilla);
                var result = await _db.SaveChangesAsync();
                if (result > 0)
                {
                    return (deleteVilla);

                }
                else
                {
                    return BadRequest("asdf");
                }
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);

            }
        }
    }
}


