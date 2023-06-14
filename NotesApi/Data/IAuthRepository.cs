namespace NotesApi.Data;

public interface IAuthRepository
{
    Task<ServiceResponse<int>> Register(User user, string password);
    Task<ServiceResponse<string>> Login(string username, string password);
    Task<ServiceResponse<int>> AdminRegister(User user, string password);
    Task<ServiceResponse<string>> AdminLogin(string username, string password);
    Task<bool> UserExists(string username);
}
