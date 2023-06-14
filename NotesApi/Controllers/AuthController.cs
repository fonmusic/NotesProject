using Microsoft.AspNetCore.Mvc;

namespace NotesApi.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly IAuthRepository _authRepo;

    public AuthController(IAuthRepository authRepo)
    {
        _authRepo = authRepo;
    }

    [HttpPost("User/Register")]
    public async Task<ActionResult<ServiceResponse<int>>> Register(UserRegisterDto request)
    {
        var response = await _authRepo.Register(
            new User { Username = request.Username }, request.Password
        );
        if (!response.Success)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }

    [HttpPost("User/Login")]
    public async Task<ActionResult<ServiceResponse<int>>> Login(UserLoginDto request)
    {
        var response = await _authRepo.Login(request.Username, request.Password);
        if (!response.Success)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }

    [HttpPost("Admin/Register")]
    public async Task<ActionResult<ServiceResponse<int>>> AdminRegister(UserRegisterDto request)
    {
        var user = new User { Username = request.Username };
        var response = await _authRepo.AdminRegister(user, request.Password);
        if (!response.Success)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }

    [HttpPost("Admin/Login")]
    public async Task<ActionResult<ServiceResponse<string>>> AdminLogin(UserLoginDto request)
    {
        var response = await _authRepo.AdminLogin(request.Username, request.Password);
        if (!response.Success)
        {
            return BadRequest(response);
        }
        return Ok(response);
    }
}
