namespace NotesApi.Services.NoteService;

public class NoteService : INoteService
{
    private readonly NotesContext _context;
    private readonly IMapper _mapper;

    public NoteService(NotesContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ServiceResponse<IEnumerable<GetNoteDto>>> GetNotes()
    {
        var serviceResponse = new ServiceResponse<IEnumerable<GetNoteDto>>();
        var notes = await _context.Notes.ToListAsync();
        serviceResponse.Data = _mapper.Map<IEnumerable<GetNoteDto>>(notes);

        return serviceResponse;
    }

    public async Task<ServiceResponse<GetNoteDto>> GetNoteById(int id)
    {
        var serviceResponse = new ServiceResponse<GetNoteDto>();
        var note = await _context.Notes.FindAsync(id);
        if (note is null)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = "Note not found.";
            return serviceResponse;
        }
        serviceResponse.Data = _mapper.Map<GetNoteDto>(note);
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetNoteDto>> CreateNote(AddNoteDto noteDto)
    {
        var serviceResponse = new ServiceResponse<GetNoteDto>();
        var note = _mapper.Map<Note>(noteDto);

        note.CreatedDate = DateTime.UtcNow;
        note.UpdatedDate = DateTime.UtcNow;
        _context.Notes.Add(note);
        await _context.SaveChangesAsync();

        serviceResponse.Data = _mapper.Map<GetNoteDto>(note);
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetNoteDto>> UpdateNote(int id, UpdateNoteDto noteDto)
    {
        var serviceResponse = new ServiceResponse<GetNoteDto>();

        var existingNote = await _context.Notes.FindAsync(id);

        if (existingNote is null)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = "Note not found.";
            return serviceResponse;
        }

        _mapper.Map(noteDto, existingNote);
        existingNote.UpdatedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        serviceResponse.Data = _mapper.Map<GetNoteDto>(existingNote);

        return serviceResponse;
    }

    public async Task<ServiceResponse<bool>> DeleteNoteById(int id)
    {
        var serviceResponse = new ServiceResponse<bool>();

        var existingNote = await _context.Notes.FindAsync(id);

        if (existingNote is null)
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
