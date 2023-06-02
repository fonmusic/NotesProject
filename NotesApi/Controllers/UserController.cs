using Microsoft.AspNetCore.Mvc;

namespace NotesApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;

    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    // GET: api/User
    [HttpGet]
    public async Task<ActionResult<ServiceResponse<IEnumerable<User>>>> GetUsers()
    {
        var serviceResponse = await _userService.GetUsers();
        return Ok(serviceResponse);
    }

    // GET: api/User/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceResponse<User>>> GetUser(int id)
    {
        var serviceResponse = await _userService.GetUserById(id);

        if (!serviceResponse.Success)
        {
            return NotFound(serviceResponse);
        }

        return Ok(serviceResponse);
    }

    // PUT: api/User/5
    [HttpPut("{id}")]
    public async Task<IActionResult> PutUser(int id, User user)
    {
        var serviceResponse = await _userService.UpdateUser(id, user);

        if (!serviceResponse.Success)
        {
            return BadRequest(serviceResponse);
        }

        return NoContent();
    }

    // POST: api/User
    [HttpPost]
    public async Task<ActionResult<ServiceResponse<User>>> PostUser(User user)
    {
        var serviceResponse = await _userService.CreateUser(user);
        return CreatedAtAction(nameof(GetUser), new { id = serviceResponse.Data.ID }, serviceResponse);
    }

    // DELETE: api/User/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var serviceResponse = await _userService.DeleteUserById(id);

        if (!serviceResponse.Success)
        {
            return NotFound(serviceResponse);
        }

        return NoContent();
    }

}
