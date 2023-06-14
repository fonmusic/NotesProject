using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace NotesApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class AdminController : ControllerBase
{
    private readonly IAdminService _adminService;

    public AdminController(IAdminService adminService)
    {
        _adminService = adminService;
    }

    [HttpGet("GetAllUsers")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ServiceResponse<IEnumerable<GetUserDto>>>> GetUsers()
    {
        var serviceResponse = await _adminService.GetUsers();
        return Ok(serviceResponse);
    }

    [HttpGet("GetUsersById/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ActionResult<ServiceResponse<GetUserDto>>> GetUser(int id)
    {
        var serviceResponse = await _adminService.GetUserById(id);

        if (!serviceResponse.Success)
        {
            return NotFound(serviceResponse);
        }

        return Ok(serviceResponse);
    }

    [HttpPut("EditUsers/{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> PutUser(int id, UpdateUserDto userDto)
    {
        var serviceResponse = await _adminService.UpdateUser(id, userDto);

        if (!serviceResponse.Success)
        {
            return BadRequest(serviceResponse);
        }

        return NoContent();
    }

    [HttpDelete("DeleteUserById{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var serviceResponse = await _adminService.DeleteUserById(id);

        if (!serviceResponse.Success)
        {
            return NotFound(serviceResponse);
        }

        return NoContent();
    }
}