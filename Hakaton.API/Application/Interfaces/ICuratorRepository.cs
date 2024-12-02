using Hakaton.API.Domen.Dto;
using Hakaton.API.Domen.Entities;

namespace Hakaton.API.Application.Interfaces;
public interface ICuratorRepository
{
    public Task<IEnumerable<Curator>> GetCurators();
    
    public Task<Curator> GetCuratorById(int id);

    public Task<int> EditCurator(CuratorDto curatorDto,int id);

    public Task<bool> DeleteCurator(int id);

    public Task<Curator> CreateCurator(CuratorDto curatorDto, int teacherId);

}