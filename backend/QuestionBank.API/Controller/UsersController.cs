using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Questionbanknew.Models;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly IConfiguration _config;
    private readonly string _connectionString;

    public UsersController(IConfiguration config)
    {
        _config = config;
        _connectionString = _config.GetConnectionString("DefaultConnection")!;
    }

    // ✅ Register endpoint
    [HttpPost("register")]
    public IActionResult Register([FromBody] RegisterRequest request)
    {
        try
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            // Check if username already exists
            var checkSql = "SELECT COUNT(*) FROM Users WHERE Username=@u";
            using (var checkCmd = new SqlCommand(checkSql, conn))
            {
                checkCmd.Parameters.AddWithValue("@u", request.Username);
                int count = (int)checkCmd.ExecuteScalar();
                if (count > 0)
                    return BadRequest(ApiResponse.Fail("Username already exists"));
            }

            // Insert new user
            var sql = "INSERT INTO Users (Username, Password, Role) VALUES (@u, @p, @r)";
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@u", request.Username);
            cmd.Parameters.AddWithValue("@p", request.Password);
            cmd.Parameters.AddWithValue("@r", request.Role);
            cmd.ExecuteNonQuery();

            return Ok(ApiResponse.Ok("User registered successfully"));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse.Fail("Registration failed", ex.Message));
        }
    }

    // ✅ Login endpoint
    // ✅ Login endpoint
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        try
        {
            using var conn = new SqlConnection(_connectionString);
            conn.Open();

            var sql = "SELECT UserID, Username, Role FROM Users WHERE Username=@u AND Password=@p";
            using var cmd = new SqlCommand(sql, conn);
            cmd.Parameters.AddWithValue("@u", request.Username);
            cmd.Parameters.AddWithValue("@p", request.Password);

            using var reader = cmd.ExecuteReader();
            if (!reader.Read())
                return Unauthorized(ApiResponse.Fail("Invalid credentials"));

            int userId = reader.GetInt32(0);
            string username = reader.GetString(1);
            string role = reader.GetString(2);

            // ✅ Generate JWT
            var claims = new[]
            {
            new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new Claim(ClaimTypes.Role, role),
            new Claim(ClaimTypes.Name, username)
        };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.Now.AddHours(1),
                signingCredentials: creds
            );

            string jwtToken = new JwtSecurityTokenHandler().WriteToken(token);

            // ✅ Return token + user object
            return Ok(ApiResponse.Ok("Login successful", new
            {
                token = jwtToken,
                user = new
                {
                    userId,
                    username,
                    role
                }
            }));
        }
        catch (Exception ex)
        {
            return StatusCode(500, ApiResponse.Fail("Login failed", ex.Message));
        }
    }

}
