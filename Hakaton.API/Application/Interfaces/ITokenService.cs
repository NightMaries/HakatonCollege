namespace Hakaton.API.Application.Interfaces;

public interface ITokenService
{
    public Task<string> CreateToken(string userLogin);
}