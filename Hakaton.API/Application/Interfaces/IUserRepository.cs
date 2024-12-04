using Hakaton.API.Domen.Dto;
using Hakaton.API.Domen.Entities;

namespace Hakaton.API.Application.Interfaces;

public interface IUserRepository
{
    public Task CreateUserAsync(UserDto userDto);
    public Task<int> EditUser(UserDto userDto,int id,bool subscription);
    public Task<User> GetUserById(int id);
    
    public Task<User> GetUserByLogin(string login);
    public Task<User> GetUserByToken(string token);
    public Task<IEnumerable<User>> GetUsers();
    public Task<bool> DeleteUser(int id);

    
}