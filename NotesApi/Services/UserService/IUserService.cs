namespace NotesApi.Services.UserService;

public interface IUserService
{
    Task<ServiceResponse<IEnumerable<User>>> GetUsers();
    Task<ServiceResponse<User>> GetUserById(int id);
    Task<ServiceResponse<User>> CreateUser(User user);
    Task<ServiceResponse<User>> UpdateUser(int id, User user);
    Task<ServiceResponse<bool>> DeleteUserById(int id);
}
