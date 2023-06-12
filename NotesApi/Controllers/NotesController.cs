using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace NotesApi.Controllers;

[Authorize]
[ApiController]
[Route("api/[controller]")]
public class NotesController : ControllerBase
{
    private readonly INoteService _noteService;

    public NotesController(INoteService noteService)
    {
        _noteService = noteService;
    }

    // GET: api/Notes
    [HttpGet]
    public async Task<ActionResult<ServiceResponse<IEnumerable<GetNoteDto>>>> GetNotes()
    {
        int userId = int.Parse(User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)!.Value);
        var serviceResponse = await _noteService.GetAllNotes(userId);
        return Ok(serviceResponse);
    }

    // GET: api/Notes/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceResponse<GetNoteDto>>> GetNote(int id)
    {
        var serviceResponse = await _noteService.GetNoteById(id);

        if (!serviceResponse.Success)
        {
            return NotFound(serviceResponse);
        }

        return Ok(serviceResponse);
    }

    // PUT: api/Notes/5
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPut("{id}")]
    public async Task<IActionResult> PutNote(int id, UpdateNoteDto noteDto)
    {
        var serviceResponse = await _noteService.UpdateNote(id, noteDto);

        if (!serviceResponse.Success)
        {
            return BadRequest(serviceResponse);
        }

        return NoContent();
    }

    // POST: api/Notes
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<ServiceResponse<GetNoteDto>>> PostNote(AddNoteDto noteDto)
    {
        var serviceResponse = await _noteService.CreateNote(noteDto);
        if (serviceResponse.Data == null)
        {
            return BadRequest(serviceResponse);
        }
        return CreatedAtAction(nameof(GetNote), new { id = serviceResponse.Data.ID }, serviceResponse);
    }

    // DELETE: api/Notes/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteNote(int id)
    {
        var serviceResponse = await _noteService.DeleteNoteById(id);

        if (!serviceResponse.Success)
        {
            return NotFound(serviceResponse);
        }

        return NoContent();
    }

}
