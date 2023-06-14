namespace NotesApi.Services.AdminService;

public interface IAdminService
{
    Task<ServiceResponse<IEnumerable<GetUserDto>>> GetUsers();
    Task<ServiceResponse<GetUserDto>> GetUserById(int id);
    Task<ServiceResponse<GetUserDto>> UpdateUser(int id, UpdateUserDto updatedUser);
    Task<ServiceResponse<bool>> DeleteUserById(int id);
}
