using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoyalVilla_API.Database;
using RoyalVilla_API.dtos;
using RoyalVilla_API.Models;
using System.Text.Json;

namespace RoyalVilla_API.Controllers
{
    [ApiController]
    [Route("api/villa")]
    public class VillaController(ApplicationDbContext db,IMapper mapper) : ControllerBase
    {

        private readonly ApplicationDbContext _db = db;
        readonly IMapper _mapper = mapper;
        [HttpGet(Name = "Getvillas")]
        public async Task<ActionResult<IEnumerable<Villa>>> GetVillas()
        {
            return Ok(await _db.Villa.ToListAsync());
        }

        //[HttpGet("{id:int}")]
        //public string GetVillaById([FromRoute] int id)
        //{
        //    return $"This is a villa with id: {id}";
        //}

        [HttpGet("{id:int}")]

        //FromRoute is the default
        public async Task<ActionResult<Villa>> GetVillaById([FromRoute] int id)
        {
            try
            {

                if (id <= 0)
                {
                    return BadRequest("id invalid");
                }
                var villa = await _db.Villa.FirstOrDefaultAsync(u => u.Id == id);
                if (villa == null)
                {
                    return NotFound($"villa with id {id} not found");
                }
                return Ok(villa);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex);
            }
        }


        [HttpPost]

        public async Task<ActionResult<Villa>> CreateVilla(VillaCreateDTO villaDTO)
        {
            try
            {
                if (villaDTO == null)
                {
                    return BadRequest("Villa is required");
                }
                var villa = _mapper.Map<Villa>(villaDTO);
                var newVilla = await _db.Villa.AddAsync(villa);
                var result = await _db.SaveChangesAsync();

                return Ok(newVilla.Entity);

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


