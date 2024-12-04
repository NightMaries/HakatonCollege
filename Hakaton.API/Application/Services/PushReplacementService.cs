
using Hakaton.API.Application.Interfaces;
using Hakaton.API.Domen.Dto;
using Hakaton.API.Domen.Entities;
using Microsoft.AspNetCore.Identity;

namespace Hakaton.API.Application.Services;

public class PushReplacementService: IPushReplacementService
{
    private readonly IUserRepository _userRepository;
    private readonly IPushReplacementUserRepository _pushReplacementUserRepositorys;

    public PushReplacementService(IUserRepository userRepository,
        IPushReplacementUserRepository pushReplacementRepositorys)
    {
        _userRepository = userRepository;
        _pushReplacementUserRepositorys = pushReplacementRepositorys;
        
    }
    
    

    public async Task<IEnumerable<PushReplacementUser>> SendingPush(ReplacementDtoGet replacementDtoGet)
    {
        IEnumerable<User> users = await _userRepository.GetUsers();
        int count = 0;
        foreach(var user in users)
        {
            if(user.Subscription)
            {    
                await _pushReplacementUserRepositorys.CreatePushReplacementUser(user.Id, replacementDtoGet.Id);
                count++; 
            }
        }
        return await _pushReplacementUserRepositorys.GetPushReplacementUsers(replacementDtoGet.Id);
    }

    
}