namespace NotesApi.Services.NoteService;

public interface INoteService
{
    Task<ServiceResponse<IEnumerable<Note>>> GetNotes();
    Task<ServiceResponse<Note>> GetNoteById(int id);
    Task<ServiceResponse<Note>> CreateNote(Note note);
    Task<ServiceResponse<Note>> UpdateNote(int id, Note note);
    Task<ServiceResponse<bool>> DeleteNoteById(int id);
}
