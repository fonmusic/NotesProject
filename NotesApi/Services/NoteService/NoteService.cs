namespace NotesApi.Services.NoteService;

public class NoteService : INoteService
{
    private readonly NotesContext _context;

    public NoteService(NotesContext context)
    {
        _context = context;
    }

    public async Task<ServiceResponse<IEnumerable<Note>>> GetNotes()
    {
        var serviceResponse = new ServiceResponse<IEnumerable<Note>>();

        serviceResponse.Data = await _context.Notes.ToListAsync();

        return serviceResponse;
    }

    public async Task<ServiceResponse<Note>> GetNoteById(int id)
    {
        var serviceResponse = new ServiceResponse<Note>();

        serviceResponse.Data = await _context.Notes.FindAsync(id);

        if (serviceResponse.Data == null)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = "Note not found.";
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<Note>> CreateNote(Note note)
    {
        var serviceResponse = new ServiceResponse<Note>();

        _context.Notes.Add(note);
        await _context.SaveChangesAsync();

        serviceResponse.Data = note;

        return serviceResponse;
    }

    public async Task<ServiceResponse<Note>> UpdateNote(int id, Note note)
    {
        var serviceResponse = new ServiceResponse<Note>();

        var existingNote = await _context.Notes.FindAsync(id);

        if (existingNote == null)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = "Note not found.";
            return serviceResponse;
        }

        existingNote.Title = note.Title;
        existingNote.Text = note.Text;
        existingNote.UpdatedDate = note.UpdatedDate;

        await _context.SaveChangesAsync();

        serviceResponse.Data = existingNote;

        return serviceResponse;
    }

    public async Task<ServiceResponse<bool>> DeleteNoteById(int id)
    {
        var serviceResponse = new ServiceResponse<bool>();

        var existingNote = await _context.Notes.FindAsync(id);

        if (existingNote == null)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = "Note not found.";
            return serviceResponse;
        }

        _context.Notes.Remove(existingNote);
        await _context.SaveChangesAsync();

        serviceResponse.Data = true;

        return serviceResponse;
    }
}
