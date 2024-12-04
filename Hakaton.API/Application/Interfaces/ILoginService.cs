using Hakaton.API.Domen.Dto;

namespace Hakaton.API.Application.Interfaces;

public interface ILoginService
{
    public Task<LoginDtoGet> CheckUserPassword(UserDto userDto);
}