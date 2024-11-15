using Microsoft.AspNetCore.Mvc;
using BankAtmBackend.Models; // Import the namespace containing LoginRequest

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    private readonly UserRepository _userRepository;

    public LoginController(UserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpPost("authenticate")]
    public async Task<IActionResult> Authenticate([FromBody] LoginData request)
    {
        if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
        {
            return BadRequest("Username and password cannot be empty.");
        }

        // Get all users and print to console for debugging
        // var allUsers = await _userRepository.GetAllUsersAsync();
        // foreach (var u in allUsers)
        // {
        //     Console.WriteLine($"Username: {u.Username}, Email: {u.Email}");
        // }
        // return Ok(allUsers);
        // Authenticate user
        var user = await _userRepository.GetUserByUsernameAsync(request.Username);
        if (user != null && user.Password == request.Password)
        {
            return Ok(new { Message = "Login successful", Token = "fake-jwt-token" });
        }

        return Unauthorized("Invalid username or password");
    }


}

public class LoginData
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}
