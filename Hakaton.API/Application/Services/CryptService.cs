using System.Security.Cryptography;
using System.Text;
using Hakaton.API.Application.Interfaces;
using Microsoft.IdentityModel.Tokens;

namespace Hakaton.API.Application.Services;
public class CryptService: ICryptService
{
    
    private SymmetricSecurityKey _key;
    public CryptService(IConfiguration config)
    {
        _key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["CryptKey"]!));
    }

    public string Encrypt(string data)
    {
        byte[] iv = new byte[16];
        using (var rng = new RNGCryptoServiceProvider())
        {
            rng.GetBytes(iv); // Генерация случайного IV
        }

        using (var aes = Aes.Create())
        {
            
            aes.Key = _key.Key;

            aes.IV = iv;
            
            try{
            using (var encryptor = aes.CreateEncryptor(aes.Key, aes.IV))
            {
                using (var ms = new System.IO.MemoryStream())
                {
                    // Записываем IV в начало
                    ms.Write(iv, 0, iv.Length);
                    using (var cryptoStream = new CryptoStream(ms, encryptor, CryptoStreamMode.Write))
                    {
                        using (var sw = new System.IO.StreamWriter(cryptoStream))
                        {
                            sw.Write(data);
                        }
                    }
                    return Convert.ToBase64String(ms.ToArray());
                }
            }}
            catch(Exception ex)
            {
                throw new Exception(ex.InnerException.Message);
            }
        }
    }

    public string Decrypt(string encryptedData)
    {
        byte[] encryptedBytes = Convert.FromBase64String(encryptedData);

        byte[] iv = new byte[16];
        Array.Copy(encryptedBytes, 0, iv, 0, iv.Length); // Извлекаем IV из зашифрованных данных

        using (var aes = Aes.Create())
        {
            aes.Key = _key.Key;
            aes.IV = iv;

            using (var decryptor = aes.CreateDecryptor(aes.Key, aes.IV))
            {
                using (var ms = new System.IO.MemoryStream(encryptedBytes, iv.Length, encryptedBytes.Length - iv.Length))
                {
                    using (var cryptoStream = new CryptoStream(ms, decryptor, CryptoStreamMode.Read))
                    {
                        using (var sr = new System.IO.StreamReader(cryptoStream))
                        {
                            return sr.ReadToEnd();
                        }
                    }
                }
            }
        }
    }

}