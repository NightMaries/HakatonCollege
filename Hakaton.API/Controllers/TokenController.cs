using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

[ApiController]
[Route("[controller]")]
public class TokenController : ControllerBase
{
    private readonly IConfiguration _config;
    private readonly IParsingScheduleForTeachersService _parcingScheduleForTeachers;

    public TokenController(IConfiguration config, IParsingScheduleForTeachersService parcingScheduleForTeachers)
    {
        _config = config;
        _parcingScheduleForTeachers = parcingScheduleForTeachers;
    }

    [HttpGet ("Parcing")]
    public IActionResult Parcing()
    {
        string path = "../Resources/ScheduleForTeachers.xlsx";
        var result = _parcingScheduleForTeachers.Parse();
        return Ok(result);
    }


    [HttpGet]
    public IActionResult GenerateToken()
    {
        var claims = new List<Claim> { new Claim(ClaimTypes.Name, "user") };
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["TokenKey"]!));

        // создаем JWT-токен
        var jwt = new JwtSecurityToken(
            issuer: _config["Jwt:Issuer"],
            audience: _config["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.Add(TimeSpan.FromDays(365)),
            signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

        return Ok(new JwtSecurityTokenHandler().WriteToken(jwt));
    }
}