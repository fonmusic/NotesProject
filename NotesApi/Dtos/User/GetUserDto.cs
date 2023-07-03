namespace NotesApi.Dtos.User;

public class GetUserDto
{
    public int Id { get; set; }
    public required string Username { get; set; }
    public bool IsDeleted { get; set; }
}
