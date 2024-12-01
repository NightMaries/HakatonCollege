using Microsoft.IdentityModel.Tokens;

namespace Hakaton.API.Application.Interfaces;

public interface ICryptService
{
    string Encrypt(string data);
    string Decrypt(string encryptedData);
}