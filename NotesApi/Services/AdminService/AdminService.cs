namespace NotesApi.Services.AdminService;

public class AdminService : IAdminService
{
    private readonly DataContext _context;
    private readonly IMapper _mapper;

    public AdminService(DataContext context, IMapper mapper)
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
        if (user is null)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = "User not found.";
            return serviceResponse;
        }
        serviceResponse.Data = _mapper.Map<GetUserDto>(user);
        return serviceResponse;
    }

    public async Task<ServiceResponse<GetUserDto>> UpdateUser(int id, UpdateUserDto updatedUser)
    {
        var serviceResponse = new ServiceResponse<GetUserDto>();

        var user = await _context.Users.FindAsync(id);

        if (user is null)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = "User not found.";
            return serviceResponse;
        }

        _mapper.Map(updatedUser, user);

        await _context.SaveChangesAsync();

        serviceResponse.Data = _mapper.Map<GetUserDto>(user);

        return serviceResponse;
    }

    public async Task<ServiceResponse<bool>> DeleteUserById(int id)
    {
        var serviceResponse = new ServiceResponse<bool>();

        var user = await _context.Users.FindAsync(id);

        if (user is null)
        {
            serviceResponse.Success = false;
            serviceResponse.Message = "User not found.";
            return serviceResponse;
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        serviceResponse.Data = true;

        return serviceResponse;
    }
}
