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

        serviceResponse.Message = "That's all your notes.";

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

        serviceResponse.Message = $"""Here's a note with Id "{id}".""";

        return serviceResponse;
    }

    public async Task<ServiceResponse<IEnumerable<GetNoteDto>>> GetNoteByWords(string words)
    {
        var serviceResponse = new ServiceResponse<IEnumerable<GetNoteDto>>();

        var notes = await _context.Notes
            .Where(n => n.User!.Id == GetUserId())
            .ToListAsync();

        char[] separators = {
            ' ', ',', ';', '-', '\t', '\n', '\r', '.', '!', '?', ':', '\"',
             '\'', '(', ')', '[', ']', '{', '}', '<', '>', '/', '\\' };

        var searchWords = words.Split(separators, StringSplitOptions.RemoveEmptyEntries)
                           .Select(w => w.Trim())
                           .ToArray();

        var filteredNotes = notes.Where(n =>
            searchWords.Any(word =>
                n.Title.Split(separators, StringSplitOptions.RemoveEmptyEntries)
                       .Any(t => t.Equals(word, StringComparison.OrdinalIgnoreCase))
                || n.Text.Split(separators, StringSplitOptions.RemoveEmptyEntries)
                       .Any(t => t.Equals(word, StringComparison.OrdinalIgnoreCase))
            )
        ).ToList();

        if (filteredNotes.Count == 0)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = $"Notes containing \"{words}\" not found.";
            return serviceResponse;
        }

        var mappedNotes = filteredNotes.Select(n => _mapper.Map<GetNoteDto>(n));

        serviceResponse.Data = mappedNotes;
        serviceResponse.Message = $"""Here's a notes containing "{words}".""";

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

        serviceResponse.Message = "The note added.";

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

        note.Title = updatedNote.Title;
        note.Text = updatedNote.Text;
        note.UpdatedDate = DateTime.UtcNow;

        await _context.SaveChangesAsync();

        serviceResponse.Data = _mapper.Map<GetNoteDto>(note);

        serviceResponse.Message = "The note updated.";

        return serviceResponse;
    }

    public async Task<ServiceResponse<bool>> DeleteNoteById(int id)
    {
        var serviceResponse = new ServiceResponse<bool>();

        var note = await _context.Notes
            .FirstOrDefaultAsync(n => n.Id == id && n.User!.Id == GetUserId());

        if (note is null)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = $"""Note with Id "{id}" not found.""";
            return serviceResponse;
        }

        _context.Notes.Remove(note);
        await _context.SaveChangesAsync();

        serviceResponse.Data = true;
        serviceResponse.Message = "The note deleted.";

        return serviceResponse;
    }
}
