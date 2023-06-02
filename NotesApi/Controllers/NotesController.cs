using Microsoft.AspNetCore.Mvc;

namespace NotesApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class NotesController : ControllerBase
{
    private readonly INoteService _noteService;

    public NotesController(INoteService noteService)
    {
        _noteService = noteService;
    }

    // GET: api/Notes
    [HttpGet]
    public async Task<ActionResult<ServiceResponse<IEnumerable<Note>>>> GetNotes()
    {
        var serviceResponse = await _noteService.GetNotes();
        return Ok(serviceResponse);
    }

    // GET: api/Notes/5
    [HttpGet("{id}")]
    public async Task<ActionResult<ServiceResponse<Note>>> GetNote(int id)
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
    public async Task<IActionResult> PutNote(int id, Note note)
    {
        var serviceResponse = await _noteService.UpdateNote(id, note);

        if (!serviceResponse.Success)
        {
            return BadRequest(serviceResponse);
        }

        return NoContent();
    }

    // POST: api/Notes
    // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
    [HttpPost]
    public async Task<ActionResult<ServiceResponse<Note>>> PostNote(Note note)
    {
        var serviceResponse = await _noteService.CreateNote(note);
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
