namespace SignalRServerApi.Helpers;

public class AuthenticateResponse
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Username { get; set; }
    public string Token { get; set; }


    public AuthenticateResponse(User user, string token)
    {
        
        Username = user.Username;
        Token = token;
    }
}
public interface IUserService
{
    AuthenticateResponse? Authenticate(AuthenticateRequest model);
    IEnumerable<User> GetAll();
    User? GetById(string id);
    void MapConnection(string user, string connectionId);
    public User GetUserbyConnectionId(string connectionId);

}

public class UserService : IUserService
{
    // users hardcoded for simplicity, store in a db with hashed passwords in production applications
    private List<User> _users = new List<User>
    {
        new User { Username = "test", Password = "test" }
    };

    private readonly IJwtUtils _jwtUtils;
    private readonly IHttpContextAccessor _context;

    public UserService(IJwtUtils jwtUtils,IHttpContextAccessor httpContext)
    {
        _jwtUtils = jwtUtils;
        _context = httpContext;
    }

    public AuthenticateResponse? Authenticate(AuthenticateRequest model)
    {
        var user = _users.SingleOrDefault(x => x.Username == model.Username && x.Password == model.Password);

        // return null if user not found
        if (user == null) return null;

        // authentication successful so generate jwt token
        var token = _jwtUtils.GenerateJwtToken(user);
        _context.HttpContext.Session.SetString("user", user.Username);
        return new AuthenticateResponse(user, token);
    }

    public IEnumerable<User> GetAll()
    {
        return _users;
    }

    
    public User? GetById(string id)
    {
        return _users.FirstOrDefault(x => x.Username == id);
    }
    public void MapConnection(string user, string connectionId)
    {
        var currentUser = _users.Find(x => x.Username == user) ?? null;
        _users.Remove(currentUser);
        if (currentUser != null)
        {
            currentUser.ConnectionId = connectionId;
            _users.Add(currentUser);
        }
    }
    public User GetUserbyConnectionId(string connectionId)
    {
        return _users.Find(x=>x.ConnectionId == connectionId);
    }
}