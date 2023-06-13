namespace NotesApi;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<Note, GetNoteDto>();
        CreateMap<AddNoteDto, Note>();
        CreateMap<UpdateNoteDto, Note>();

        CreateMap<User, GetUserDto>();
        CreateMap<AddUserDto, User>();
        CreateMap<UpdateUserDto, User>();
    }
}
