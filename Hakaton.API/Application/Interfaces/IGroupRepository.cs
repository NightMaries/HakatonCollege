using Hakaton.API.Domen.Dto;
using Hakaton.API.Domen.Entities;

namespace Hakaton.API.Application.Interfaces;

public interface IGroupRepository
{
    public Task<IEnumerable<Group>> GetGroups();
    
    public Task<Group> GetGroupById(int id);

    public Task<int> EditGroup(GroupDto groupDto,int id);

    public Task<bool> DeleteGroup(int id);

    public Task<Group> CreateGroup(GroupDto groupDto, int teacherId);

}