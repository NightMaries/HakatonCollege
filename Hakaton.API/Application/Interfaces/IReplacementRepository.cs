using Hakaton.API.Domen.Dto;
using Hakaton.API.Domen.Entities;

public interface IReplacementRepository
{
    public Task<IEnumerable<ReplacementDto>> GetReplacements();
    public Task<int> EditReplacement (ReplacementDto replacementDto, int id);
    public Task<bool> DeleteReplacement(int id);
    Task<int> CreateReplacement(ReplacementDto replacement);
    Task<ReplacementDto> GetReplacementById(int id);
}