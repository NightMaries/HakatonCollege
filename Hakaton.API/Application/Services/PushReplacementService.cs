
using Hakaton.API.Application.Interfaces;
using Hakaton.API.Domen.Entities;
using Microsoft.AspNetCore.Identity;

namespace Hakaton.API.Application.Services;

public class PushReplacementService
{
    private readonly IUserRepository _userRepository;
    private readonly IReplacementRepository _replacementRepository;
    private readonly IPushReplacementRepository _pushReplacementRepositorys;

    public PushReplacementService(IUserRepository userRepository,IReplacementRepository replacementRepository,
        IPushReplacementRepository pushReplacementRepositorys)
    {
        _userRepository = userRepository;
        _replacementRepository = replacementRepository;
        _pushReplacementRepositorys = pushReplacementRepositorys;
    }  
    

    public async Task<int> SendingPush(Replacement replacement)
    {
        IEnumerable<User> users = await _userRepository.GetUsers();
        int count = 0;
        foreach(var user in users)
        {
            if(user.Subscription)
            {    
                await _pushReplacementRepositorys.CreatePushReplacementUser(user, replacement);
                count++; 
            }
        }
        return count;
    }

    
}