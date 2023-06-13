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

    [HttpGet("GetAll")]
    public async Task<ActionResult<ServiceResponse<IEnumerable<GetNoteDto>>>> GetNotes()
    {
        var serviceResponse = await _noteService.GetAllNotes();
        return Ok(serviceResponse);
    }

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

    [HttpGet("title/{title}")]
    public async Task<ActionResult<ServiceResponse<IEnumerable<GetNoteDto>>>> GetNoteByTitle([FromRoute] string title)
    {
        var serviceResponse = await _noteService.GetNoteByTitle(title);

        if (!serviceResponse.Success)
        {
            return NotFound(serviceResponse);
        }

        return Ok(serviceResponse);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutNote(int id, UpdateNoteDto updatetNote)
    {
        var serviceResponse = await _noteService.UpdateNote(id, updatetNote);

        if (!serviceResponse.Success)
        {
            return BadRequest(serviceResponse);
        }

        return Ok(serviceResponse);
    }

    [HttpPost]
    public async Task<ActionResult<ServiceResponse<GetNoteDto>>> PostNote(AddNoteDto newNote)
    {
        var serviceResponse = await _noteService.AddNote(newNote);
        if (serviceResponse.Data is null)
        {
            return BadRequest(serviceResponse);
        }
        return CreatedAtAction(nameof(GetNote), new { id = serviceResponse.Data.ID }, serviceResponse);
    }

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
