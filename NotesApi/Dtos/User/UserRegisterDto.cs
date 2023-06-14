namespace NotesApi.Dtos.User;

public class UserRegisterDto
{
    public required string Username { get; set; }
    public required string Password { get; set; }
    public bool IsAdmin { get; set; }
}
