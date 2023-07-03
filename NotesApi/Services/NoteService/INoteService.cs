namespace NotesApi.Services.NoteService;

public interface INoteService
{
    Task<ServiceResponse<IEnumerable<GetNoteDto>>> GetAllNotes();
    Task<ServiceResponse<GetNoteDto>> GetNoteById(int id);
    Task<ServiceResponse<IEnumerable<GetNoteDto>>> GetNoteByWords(string words);
    Task<ServiceResponse<GetNoteDto>> AddNote(AddNoteDto newNote);
    Task<ServiceResponse<GetNoteDto>> UpdateNote(int id, UpdateNoteDto updatedNote);
    Task<ServiceResponse<bool>> DeleteNoteById(int id);
}
