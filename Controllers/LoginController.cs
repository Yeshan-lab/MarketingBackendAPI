using Microsoft.AspNetCore.Mvc;
using MyBackendApi.Models;
using Microsoft.EntityFrameworkCore;
using MyBackendApi.Data;

namespace MyBackendApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserLoginController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UserLoginController(AppDbContext context)
        {
            _context = context;
        }

        // ✅ Login (Authenticate)
        [HttpPost("authenticate")]
        public async Task<IActionResult> Authenticate([FromBody] LoginRequest request)
        {
            var user = await _context.UserLogins
                .FirstOrDefaultAsync(u => u.Username == request.Username && u.Password == request.Password);

            if (user == null)
            {
                return Unauthorized(new { message = "Invalid username or password" });
            }

            // Return user info including role
            return Ok(new
            {
                user.Username,
                user.Role
            });
        }

        // ✅ Create new login (POST)
        [HttpPost]
        public async Task<ActionResult<UserLogin>> PostUserLogin(UserLogin userLogin)
        {
            // Check if username already exists
            var existingUser = await _context.UserLogins
                .FirstOrDefaultAsync(u => u.Username == userLogin.Username);

            if (existingUser != null)
            {
                return Conflict(new { message = "Username already exists." });
            }

            _context.UserLogins.Add(userLogin);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserLogin", new { username = userLogin.Username }, userLogin);
        }

        // ✅ Get all logins (GET)
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserLogin>>> GetUserLogins()
        {
            return await _context.UserLogins.ToListAsync();
        }

        // ✅ Get a specific login by username (GET)
        [HttpGet("{username}")]
        public async Task<ActionResult<UserLogin>> GetUserLogin(string username)
        {
            var userLogin = await _context.UserLogins
                .FirstOrDefaultAsync(u => u.Username == username);

            if (userLogin == null)
            {
                return NotFound();
            }

            return userLogin;
        }

        // ✅ Update login (PUT)
        [HttpPut("{username}")]
        public async Task<IActionResult> PutUserLogin(string username, [FromBody] UserLogin userLogin)
        {
            if (username != userLogin.Username)
            {
                return BadRequest();
            }

            _context.Entry(userLogin).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserLoginExists(username))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // ✅ Delete login (DELETE)
        [HttpDelete("{username}")]
        public async Task<IActionResult> DeleteUserLogin(string username)
        {
            var userLogin = await _context.UserLogins
                .FirstOrDefaultAsync(u => u.Username == username);
            if (userLogin == null)
            {
                return NotFound();
            }

            _context.UserLogins.Remove(userLogin);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool UserLoginExists(string username)
        {
            return _context.UserLogins.Any(e => e.Username == username);
        }

        // ✅ Class for login request
        public class LoginRequest
        {
            public string Username { get; set; }
            public string Password { get; set; }
        }
    }
}
