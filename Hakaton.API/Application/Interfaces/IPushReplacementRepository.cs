
using Hakaton.API.Domen.Entities;

namespace Hakaton.API.Application.Interfaces;
public interface IPushReplacementRepository
{
    public Task<int> CreatePushReplacementUser(User user, Replacement replacement);

    public Task<int> EditPushReplacementUser(PushReplacementUser pushReplacementUser,int id);

    public Task<bool> DeletePushReplacementUser(int id);

    public Task<IEnumerable<PushReplacementUser>> GetPushReplacementUsers();

    public Task<PushReplacementUser> GetPushReplacementUserById(int id);

}