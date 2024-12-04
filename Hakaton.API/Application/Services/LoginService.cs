using System.Security.Cryptography;
using System.Text;
using Hakaton.API.Application.Interfaces;
using Hakaton.API.Domen.Dto;
using Hakaton.API.Domen.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using SqlKata;

namespace  Hakaton.API.Application.Services;

public class LoginService: ILoginService
{
    private readonly SymmetricSecurityKey _key;
    private readonly ITokenService _token;
    private readonly ILogger<LoginService> _logger;
    private readonly IUserRepository _repository;
    private readonly ICryptService _crypt;
    private HMACSHA256 hmac = new HMACSHA256();

    public LoginService(
            ITokenService token,
            ILogger<LoginService> logger,
            IUserRepository repository,
            IConfiguration config,
            ICryptService crypt)
    {
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["TokenKey"]!));
        _token = token;
        _logger = logger;
        _repository = repository;
        _crypt = crypt;
    }

    
    
    
   
    public async Task<LoginDtoGet> CheckUserPassword(UserDto userDto)
    {

        User user = await _repository.GetUserByLogin(userDto.Login);

        string password = user.PasswordHash;
        
        // Расшифровка пароля
        string decryptedPassword = _crypt.Decrypt(password);
        
        
        if(decryptedPassword != userDto.Password) throw new Exception("Неверный пароль");
        var jwt = await _token.CreateToken(userDto.Login);
        var userGet  = await _repository.GetUserByLogin(userDto.Login);
        LoginDtoGet loginDtoGet = new LoginDtoGet
        {
            Id = userGet.Id,
            RoleId = userGet.RoleId

        };
        return loginDtoGet;
        
    }

}