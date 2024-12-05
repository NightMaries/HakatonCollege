using System.Security.Cryptography;
using System.Text;
using Azure.Core.Pipeline;
using Hakaton.API.Application.Interfaces;
using Hakaton.API.Application.Repositories;
using Hakaton.API.Application.Services;
using Hakaton.API.Domen.Dto;
using Hakaton.API.Domen.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Hakaton.API.Controllers;
[ApiController]
[Route("[controller]")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _repository;
    private readonly ILoginService _loginService;
    HMACSHA256 hmac = new HMACSHA256();

    public UserController(IUserRepository repository, ILoginService loginService)
    {
        _repository = repository;
        _loginService = loginService;
    }
    //[Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateUser(UserDto userDto)
    {
        var result = _repository.CreateUserAsync(userDto);
        return Ok(userDto);//$"http://localhost:5057/User{user.Id}",result
    }

    [HttpGet ("{id}")]
    public async Task<IActionResult> FindUserById(int id)
    {
        var user = await _repository.GetUserById(id);
        if(user is null)
            return NotFound();
        return Ok(user);
    }
    /*
    [HttpGet ("login")]
    public async Task<IActionResult> FindUserByLogin(string login)
    {
        var user = await _repository.GetUserByLogin(login);
        if(user is null)
            return NotFound();
        return Ok(user);
    }
    [HttpGet ("token")]
    public async Task<IActionResult> FindUserByToken(string token)
    {
        var user = await _repository.GetUserByToken(token);
        if(user is null)
            return NotFound();
        return Ok(user);
    }*/

    [HttpGet]
    public async Task<IActionResult> GetUsers()
    {
        var users = await _repository.GetUsers();
        if(users is null)
            return NotFound();
        return Ok(users);
    }

    [HttpPut]
    public async Task<IActionResult> EditUser(UserDto userDto,int id,bool subscription)
    {
        var result = await _repository.EditUser(userDto,id,subscription);
        if (result == 0)
            return NotFound();
        return Ok(result);
    }
    //[Authorize]
    [HttpDelete]
    public async Task<IActionResult> RemoveUser(int id)
    {
        var result = await _repository.DeleteUser(id);
        if (result is false)
            return BadRequest();
        return Ok(result);
    }

    [HttpGet("Login")]
    public async Task<IActionResult> CheckPassword([FromQuery]UserDto userDto)
    {
        var result = await _loginService.CheckUserPassword(userDto);
            if(result is null ) throw new  Exception("Неверный пароль или логин");
        return Ok(result);
    }
}