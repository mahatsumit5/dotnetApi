using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RoyalVilla_API.Database;
using RoyalVilla_API.dtos;
using RoyalVilla_API.services;

namespace RoyalVilla_API.Controllers;


[Route("api/auth")]
[ApiController]
public class AuthController(IAuthService authservice) : ControllerBase
{

    private readonly IAuthService _authService = authservice;

    [HttpPost]
    public async Task<ActionResult<ApiResponse<UserDTO>>> Register([FromBody]RegisterRequestDTO registerReqDTO)
    {
        var email=registerReqDTO.Email;
        if (registerReqDTO == null)
        {
            return BadRequest(ApiResponse<object>.SendErrorResponse(401, "Registeration data is required"));
        }
        if(await _authService.DoesEmailExistsAsync(email))
        {
            return Conflict(ApiResponse<object>.SendErrorResponse(401, $"User with email {email} already exist"));

        }

        var user = await _authService.RegisterAsync(registerReqDTO);
        if (user == null)
        {
            return BadRequest(ApiResponse<object>.SendErrorResponse(401, "registeration failed"));
        }
        var response = ApiResponse<UserDTO>.SendSuccessResponse(user, "user registered sucessfully");

        //return CreatedAtAction("Register",response);
        return CreatedAtAction(nameof(Register), response);

    }
}

