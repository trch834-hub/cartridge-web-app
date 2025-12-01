using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CartridgeWebApp.Models;

namespace CartridgeWebApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AuthController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            try
            {
                // ✅ ПРОСТАЯ ПРОВЕРКА - как работало раньше
                if (request.Username == "admin" && request.Password == "12345")
                {
                    return Ok(new
                    {
                        message = "✅ Успешный вход",
                        role = "Admin",
                        username = "admin"
                    });
                }

                if (request.Username == "user1" && request.Password == "11111")
                {
                    return Ok(new
                    {
                        message = "✅ Успешный вход",
                        role = "User",
                        username = "user1"
                    });
                }

                return Unauthorized(new { message = "❌ Неверный логин или пароль" });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "Ошибка сервера: " + ex.Message });
            }
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
}