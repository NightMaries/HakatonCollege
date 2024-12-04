
using Hakaton.API.Domen.Dto;
using Hakaton.API.Domen.Entities;

namespace Hakaton.API.Application.Interfaces;
public interface IPushReplacementUserRepository
{
    public Task<int> CreatePushReplacementUser(int userId , int replacementId);

    public Task<int> EditPushReplacementUser(PushReplacementUser pushReplacementUser,int id);

    public Task<bool> DeletePushReplacementUser(int id);

    public Task<IEnumerable<PushReplacementUser>> GetPushReplacementUsers(int replacementId);

    public Task<PushReplacementUser> GetPushReplacementUserById(int id);

}