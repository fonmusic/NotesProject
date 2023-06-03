namespace NotesApi.Services.UserService;

public interface IUserService
{
    Task<ServiceResponse<IEnumerable<GetUserDto>>> GetUsers();
    Task<ServiceResponse<GetUserDto>> GetUserById(int id);
    Task<ServiceResponse<GetUserDto>> CreateUser(AddUserDto userDto);
    Task<ServiceResponse<GetUserDto>> UpdateUser(int id, UpdateUserDto userDto);
    Task<ServiceResponse<bool>> DeleteUserById(int id);
}
