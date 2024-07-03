using Microsoft.AspNetCore.Identity.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using OnlineMovieTicketingApplication.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

[Route("api/[controller]")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly MovieContext _context;
    private readonly JWTsettings _jwtSettings;

    public UsersController(MovieContext context, IOptions<JWTsettings> jwtSettings)
    {
        _context = context;
        _jwtSettings = jwtSettings.Value;
    }
    


    // POST: api/users/register
    [HttpPost("register")]
    public async Task<ActionResult<User>> Register(User user)
    {
        
        if (await _context.Users.AnyAsync(u => u.UserName == user.UserName))
        {
            return BadRequest("Username already exists.");
        }
        user.Id = Guid.NewGuid().ToString();
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
    }




    // POST: api/users/login
    [HttpPost("login")]
    public async Task<ActionResult<string>> Login(OnlineMovieTicketingApplication.Models.LoginRequest loginRequest)
    {
        var dbUser = await _context.Users
            .FirstOrDefaultAsync(u => u.UserName == loginRequest.UserName && u.Password == loginRequest.Password);

        if (dbUser == null)
        {
            return Unauthorized();
        }

        
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_jwtSettings.Key);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
            new Claim(ClaimTypes.Name, dbUser.UserName),
            new Claim(ClaimTypes.NameIdentifier, dbUser.Id.ToString()),
            new Claim("Location", dbUser.Location)
        }),
            Expires = DateTime.UtcNow.AddDays(_jwtSettings.ExpireDays),
            Issuer = _jwtSettings.Issuer,
            Audience = _jwtSettings.Audience,
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        var tokenString = tokenHandler.WriteToken(token);

        return Ok(new { Token = tokenString, Location = dbUser.Location, Id = dbUser.Id });
    }


    // GET: api/users/{id}
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(string id)
    {
        var user = await _context.Users.FindAsync(id);

        if (user == null)
        {
            return NotFound();
        }

        return user;
    }
    // PUT: api/users/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateUser(string id, User user)
    {
        if (id != user.Id)
        {
            return BadRequest();
        }

        _context.Entry(user).State = EntityState.Modified;

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UserExists(id))
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

    private bool UserExists(string id)
    {
        return _context.Users.Any(e => e.Id == id);
    }
    


}

