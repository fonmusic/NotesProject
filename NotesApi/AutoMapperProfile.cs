namespace NotesApi;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Note, GetNoteDto>();
        CreateMap<Note, AddNoteDto>();
        CreateMap<Note, UpdateNoteDto>();

        CreateMap<User, GetUserDto>();
        CreateMap<User, AddUserDto>();
        CreateMap<User, UpdateUserDto>();
    }
}
