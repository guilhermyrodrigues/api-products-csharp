using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly string chaveJwt = "sua-chave-secreta-super-segura";

    public AuthController(AppDbContext context)
    {
        _context = context;
    }

    [HttpPost("register")]
    public IActionResult Register([FromBody] User user)
    {
        if (user == null || string.IsNullOrEmpty(user.Name) || string.IsNullOrEmpty(user.Email) || string.IsNullOrEmpty(user.Password))
        {
            return BadRequest("Dados inválidos.");
        }

        _context.Users.Add(user);
        _context.SaveChanges();

        return Ok("Usuário registrado com sucesso.");
    }

    [HttpPost("login")]
    public IActionResult Login([FromBody] User credentials)
    {
        var user = _context.Users.FirstOrDefault(x => 
            x.Email == credentials.Email && x.Password == credentials.Password);
        if (user == null) return Unauthorized("Credenciais inválidas");

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(chaveJwt);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(
            [
                new Claim(ClaimTypes.Name, user.Email)
            ]),
            Expires = DateTime.UtcNow.AddHours(2),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        return Ok(new { token = tokenHandler.WriteToken(token) });
    }
}