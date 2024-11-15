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

    [HttpGet("userinfo/{username}")]
    public async Task<IActionResult> GetUserInfo(string username)
    {
        try
        {
            var user = await _userRepository.GetUserByUsernameAsync(username);
            if (user == null)
            {
                return NotFound("User not found");
            }

            return Ok(new
            {
                Username = user.Username,
                Email = user.Email,
                AccountBalance = user.AccountBalance, // Make sure the field matches the model property
                RecentTransactions = user.RecentTransactions.Select(transaction => new
                {
                    Date = transaction.Date,
                    Amount = transaction.Amount,
                    Description = transaction.Description
                }).ToList()
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
    [HttpPost("deposit")]
    public async Task<IActionResult> Deposit([FromBody] TransactionRequest request)
    {
        try
        {
            var user = await _userRepository.GetUserByUsernameAsync(request.Username);
            if (user == null)
            {
                return NotFound("User not found");
            }

            // Update the account balance
            user.AccountBalance += request.Amount;

            // Add a transaction for deposit
            user.RecentTransactions.Add(new Transaction
            {
                Date = DateTime.Now.ToString("yyyy-MM-dd"),
                Amount = request.Amount,
                Description = "Deposit"
            });

            // Save changes
            await _userRepository.UpdateUserAsync(user);

            return Ok(new
            {
                Message = "Deposit successful",
                NewBalance = user.AccountBalance
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }

    [HttpPost("withdraw")]
    public async Task<IActionResult> Withdraw([FromBody] TransactionRequest request)
    {
        try
        {
            var user = await _userRepository.GetUserByUsernameAsync(request.Username);
            if (user == null)
            {
                return NotFound("User not found");
            }

            // Check if the user has sufficient balance
            if (user.AccountBalance < request.Amount)
            {
                return BadRequest("Insufficient balance");
            }

            // Update the account balance
            user.AccountBalance -= request.Amount;

            // Add a transaction for withdrawal
            user.RecentTransactions.Add(new Transaction
            {
                Date = DateTime.Now.ToString("yyyy-MM-dd"),
                Amount = -request.Amount,
                Description = "Withdrawal"
            });

            // Save changes
            await _userRepository.UpdateUserAsync(user);

            return Ok(new
            {
                Message = "Withdrawal successful",
                NewBalance = user.AccountBalance
            });
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"Internal server error: {ex.Message}");
        }
    }
}

public class LoginData
{
    public string Username { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
}

// Define the request model for deposit and withdrawal
public class TransactionRequest
{
    public string Username { get; set; } = string.Empty;
    public double Amount { get; set; }
}