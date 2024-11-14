using Microsoft.AspNetCore.Mvc;
using BasicDotnetApp.Models; // Import the namespace containing LoginRequest

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    [HttpPost("authenticate")]
    public IActionResult Authenticate([FromBody] LoginRequest request)
    {
        if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
        {
            return BadRequest("Username and password cannot be empty.");
        }

        // For simplicity, assume a hardcoded username and password
        if (request.Username == "a" && request.Password == "a")
        {
            return Ok(new { Message = "Login successful", Token = "fake-jwt-token" });
        }

        return Unauthorized("Invalid username or password");
    }
}
namespace BasicDotnetApp.Models
{
    public class LoginRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}

