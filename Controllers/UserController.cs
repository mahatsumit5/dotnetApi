using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RoyalVilla_API.Database;
using RoyalVilla_API.dtos;

namespace RoyalVilla_API.Controllers
{
    [ApiController]
    [Route("api/user")]
    public class UserController(IMapper mapper, ApplicationDbContext db) : ControllerBase
    {
        private readonly IMapper _mapper = mapper;
        private readonly ApplicationDbContext _db = db;

        [HttpDelete]

        public async Task<ActionResult<ApiResponse<bool>>> DeleteAllUsers()
        {
          await  _db.Users.ExecuteDeleteAsync();
            var status = await _db.SaveChangesAsync();
            return status > 0 
                ? Ok(ApiResponse<bool>.SendSuccessResponse(true,"All data deleted."))
                : BadRequest(ApiResponse<object>.SendErrorResponse(400,"Bad request"));
        }

    }
}
