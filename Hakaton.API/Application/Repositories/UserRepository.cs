
using System.Security.Cryptography;
using System.Text;
using Hakaton.API.Application.Interfaces;
using Hakaton.API.Domen.Dto;
using Hakaton.API.Domen.Entities;
using Hakaton.API.Infrustructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.VisualBasic;
using SqlKata;
using SqlKata.Execution;

namespace Hakaton.API.Application.Repositories;

public class UserRepository : IUserRepository
{
    private readonly QueryFactory _query;
    private readonly ITokenService _tokenService;
    private readonly ICryptService _cryptService;
    
    private HMACSHA256 hmac = new HMACSHA256();
    public UserRepository(HakatonContext hakatonContext, ITokenService tokenService,ICryptService cryptService)
    {
        _query = hakatonContext.PostgresQueryFactory;
        _tokenService = tokenService;
        _cryptService = cryptService;
    }
    public async Task CreateUserAsync(UserDto userDto)
    {
        if(userDto.Password is null || userDto.Login is null)
            throw new Exception("Заполните поля");

        
        var query =  _query.Query("Users").AsInsert(new {
            Login = userDto.Login,
            PasswordHash = _cryptService.Encrypt(userDto.Password),
            Token = await _tokenService.CreateToken(userDto.Login),
            RoleId = 1,
            Subscription = false
            });
        
        
        await _query.ExecuteAsync(query);
    }

    public async Task<bool> DeleteUser(int id)
    {
        var result = await _query.Query("Users").Where("Id", id).DeleteAsync();
        if (result != 1)
            throw new Exception("Преподаватель не найден");
        
            return false;
    }

    public async Task<int> EditUser(UserDto userDto,int id,bool subscription)
    {
    
        int affected = await _query.Query("Users")
                .Where("Id", id)
                .UpdateAsync(new { Login = userDto.Login,
                    PasswordHash = _cryptService.Encrypt(userDto.Password),
                    Token = await _tokenService.CreateToken(userDto.Login),
                    RoleId = 1,
                    Subscription = subscription
                });
        return affected;
    }

    public async Task<User> GetUserById(int id)
    {
        var query = _query.Query("Users")
            .Where("Id",id)
            .Select("Users.Id","Login","PasswordHash","Token","Users.RoleId","Subscription");

        var result = await _query.FirstOrDefaultAsync<User>(query);
        if(result is null) throw new Exception("Пользователь не найден");
        return result;
    }
    public async Task<User> GetUserByLogin(string login)
    {
        var query = _query.Query("Users")
            .Where("Login",login)
            .Select("Id","Login","PasswordHash","Token","Users.RoleId","Subscription");

        var result = await _query.FirstOrDefaultAsync<User>(query);
        if(result is null) throw new Exception("Пользователь не найден");
        return result;
    }
    public async Task<User> GetUserByToken(string token)
    {
        var query = _query.Query("Users")
            .Where("Token",token)
            .Select("Id","Login","PasswordHash","Token","Users.RoleId","Subscription");

        var result = await _query.FirstOrDefaultAsync<User>(query);
        if(result is null) throw new Exception("Пользователь не найден");
        return result;
    }
    public async Task<IEnumerable<User>> GetUsers()
    {
        var query = _query.Query("Users")
                    .Join("Roles","Roles.Id","Users.RoleId")
                    .Select("Users.Id","Login","PasswordHash","Token","RoleId","Subscription");
        var result = await _query.GetAsync<User>(query);
        if(result is null) throw new Exception("Ошибка в запросе");
        return result;
        }
}