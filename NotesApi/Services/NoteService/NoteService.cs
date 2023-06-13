using System.Security.Claims;

namespace NotesApi.Services.NoteService;

public class NoteService : INoteService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public NoteService(DataContext context, IMapper mapper, IHttpContextAccessor httpContextAccessor)
    {
        _context = context;
        _mapper = mapper;
        _httpContextAccessor = httpContextAccessor;
    }

    private int GetUserId() => int.Parse(_httpContextAccessor.HttpContext!.User
             .FindFirstValue(ClaimTypes.NameIdentifier)!);

    public async Task<ServiceResponse<IEnumerable<GetNoteDto>>> GetAllNotes()
    {
        var serviceResponse = new ServiceResponse<IEnumerable<GetNoteDto>>();
        var notes = await _context.Notes
            .Where(n => n.User!.Id == GetUserId())
            .ToListAsync();
        serviceResponse.Data = _mapper.Map<IEnumerable<GetNoteDto>>(notes);

        return serviceResponse;
    }

    public async Task<ServiceResponse<GetNoteDto>> GetNoteById(int id)
    {
        var serviceResponse = new ServiceResponse<GetNoteDto>();
        var note = await _context.Notes
            .FirstOrDefaultAsync(n => n.Id == id && n.User!.Id == GetUserId());
        if (note is null)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = $"""Note with Id "{id}" not found.""";
            return serviceResponse;
        }
        serviceResponse.Data = _mapper.Map<GetNoteDto>(note);
        return serviceResponse;
    }

    public async Task<ServiceResponse<IEnumerable<GetNoteDto>>> GetNoteByTitle(string title)
    {
        var serviceResponse = new ServiceResponse<IEnumerable<GetNoteDto>>();

        var notes = await _context.Notes
            .Where(n => n.User!.Id == GetUserId())
            .ToListAsync();

        var filteredNotes = notes.Where(n =>
            n.Title.IndexOf(title, StringComparison.OrdinalIgnoreCase) >= 0 ||
            title.Split(' ').Any(word => n.Title.IndexOf(word, StringComparison.OrdinalIgnoreCase) >= 0)
        ).ToList();

        if (filteredNotes.Count == 0)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = $"Notes with Title \"{title}\" not found.";
            return serviceResponse;
        }

        var mappedNotes = filteredNotes.Select(n => _mapper.Map<GetNoteDto>(n));
        serviceResponse.Data = mappedNotes;

        return serviceResponse;
    }

    public async Task<ServiceResponse<GetNoteDto>> AddNote(AddNoteDto newNote)
    {
        var serviceResponse = new ServiceResponse<GetNoteDto>();
        var note = _mapper.Map<Note>(newNote);
        note.User = await _context.Users.FirstOrDefaultAsync(u => u.Id == GetUserId());

        note.CreatedDate = DateTime.UtcNow;
        note.UpdatedDate = DateTime.UtcNow;

        _context.Notes.Add(note);
        await _context.SaveChangesAsync();

        serviceResponse.Data = _mapper.Map<GetNoteDto>(note);

        return serviceResponse;
    }

    public async Task<ServiceResponse<GetNoteDto>> UpdateNote(int id, UpdateNoteDto updatedNote)
    {
        var serviceResponse = new ServiceResponse<GetNoteDto>();

        var note = await _context.Notes
            .Include(n => n.User)
            .FirstOrDefaultAsync(n => n.Id == updatedNote.Id);

        if (note is null || note.User!.Id != GetUserId())
        {
            serviceResponse.Success = false;
            serviceResponse.Message = $"""Note with Id "{id}" not found.""";
            return serviceResponse;
        }

        // _mapper.Map(updatedNote, note);
        note.Title = updatedNote.Title;
        note.Text = updatedNote.Text;
        note.UpdatedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        serviceResponse.Data = _mapper.Map<GetNoteDto>(note);

        return serviceResponse;
    }

    public async Task<ServiceResponse<bool>> DeleteNoteById(int id)
    {
        var serviceResponse = new ServiceResponse<bool>();

        var existingNote = await _context.Notes
            .FirstOrDefaultAsync(n => n.Id == id && n.User!.Id == GetUserId());

        if (existingNote is null)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = $"""Note with Id "{id}" not found.""";
            return serviceResponse;
        }

        _context.Notes.Remove(existingNote);
        await _context.SaveChangesAsync();

        serviceResponse.Data = true;

        return serviceResponse;
    }
}
