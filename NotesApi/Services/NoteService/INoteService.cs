namespace NotesApi.Services.NoteService;

public interface INoteService
{
    Task<ServiceResponse<IEnumerable<GetNoteDto>>> GetNotes();
    Task<ServiceResponse<GetNoteDto>> GetNoteById(int id);
    Task<ServiceResponse<GetNoteDto>> CreateNote(AddNoteDto noteDto);
    Task<ServiceResponse<GetNoteDto>> UpdateNote(int id, UpdateNoteDto noteDto);
    Task<ServiceResponse<bool>> DeleteNoteById(int id);
}
