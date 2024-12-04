using Hakaton.API.Domen.Dto;

namespace Hakaton.API.Application.Interfaces;

public interface ILoginService
{
    public Task<int> CheckUserPassword(UserDto userDto);
}