namespace NotesApi.Services.UserService;

public class UserService : IUserService
{
    private readonly NotesContext _context;
    private readonly IMapper _mapper;

    public UserService(NotesContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ServiceResponse<IEnumerable<GetUserDto>>> GetUsers()
    {
        var serviceResponse = new ServiceResponse<IEnumerable<GetUserDto>>();
        var users = await _context.Users.ToListAsync();        
        serviceResponse.Data = _mapper.Map<IEnumerable<GetUserDto>>(users);

        return serviceResponse;
    }

    public async Task<ServiceResponse<GetUserDto>> GetUserById(int id)
    {
        var serviceResponse = new ServiceResponse<GetUserDto>();
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = "User not found.";
            return serviceResponse;
        }
        serviceResponse.Data = _mapper.Map<GetUserDto>(user);
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetUserDto>> CreateUser(AddUserDto userDto)
    {
        var serviceResponse = new ServiceResponse<GetUserDto>();
        var user = _mapper.Map<User>(userDto);

        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        serviceResponse.Data = _mapper.Map<GetUserDto>(user);;
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetUserDto>> UpdateUser(int id, UpdateUserDto userDto)
    {
        var serviceResponse = new ServiceResponse<GetUserDto>();

        var existingUser = await _context.Users.FindAsync(id);

        if (existingUser == null)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = "User not found.";
            return serviceResponse;
        }

        _mapper.Map(userDto, existingUser);

        await _context.SaveChangesAsync();

        serviceResponse.Data = _mapper.Map<GetUserDto>(existingUser);

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
