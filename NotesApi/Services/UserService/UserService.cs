namespace NotesApi.Services.UserService;

public class UserService : IUserService
{
    private readonly NotesContext _context;

    public UserService(NotesContext context)
    {
        _context = context;
    }

    public async Task<ServiceResponse<IEnumerable<User>>> GetUsers()
    {
        var serviceResponse = new ServiceResponse<IEnumerable<User>>();

        serviceResponse.Data = await _context.Users.ToListAsync();

        return serviceResponse;
    }

    public async Task<ServiceResponse<User>> GetUserById(int id)
    {
        var serviceResponse = new ServiceResponse<User>();

        serviceResponse.Data = await _context.Users.FindAsync(id);

        if (serviceResponse.Data == null)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = "User not found.";
        }

        return serviceResponse;
    }

    public async Task<ServiceResponse<User>> CreateUser(User user)
    {
        var serviceResponse = new ServiceResponse<User>();

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        serviceResponse.Data = user;

        return serviceResponse;
    }

    public async Task<ServiceResponse<User>> UpdateUser(int id, User user)
    {
        var serviceResponse = new ServiceResponse<User>();

        var existingUser = await _context.Users.FindAsync(id);

        if (existingUser == null)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = "User not found.";
            return serviceResponse;
        }

        existingUser.Username = user.Username;
        existingUser.Password = user.Password;

        await _context.SaveChangesAsync();

        serviceResponse.Data = existingUser;

        return serviceResponse;
    }

    public async Task<ServiceResponse<bool>> DeleteUserById(int id)
    {
        var serviceResponse = new ServiceResponse<bool>();

        var existingUser = await _context.Users.FindAsync(id);

        if (existingUser == null)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = "User not found.";
            return serviceResponse;
        }

        _context.Users.Remove(existingUser);
        await _context.SaveChangesAsync();

        serviceResponse.Data = true;

        return serviceResponse;
    }
}
