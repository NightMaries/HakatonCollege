using Hakaton.API.Domen.Dto;
using Hakaton.API.Domen.Entities;

public interface IReplacementRepository
{
    public Task<IEnumerable<ReplacementDtoGet>> GetReplacements();
    public Task<int> EditReplacement (ReplacementDtoPost replacementDtoPost, int id);
    public Task<bool> DeleteReplacement(int id);
    Task<IEnumerable<PushReplacementUser>> CreateReplacement(ReplacementDtoPost replacementDtoPost);
    Task<ReplacementDtoGet> GetReplacementById(int id);
}