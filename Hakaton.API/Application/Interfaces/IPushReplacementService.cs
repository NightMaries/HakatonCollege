using Hakaton.API.Domen.Dto;
using Hakaton.API.Domen.Entities;

public interface IPushReplacementService
{
    public Task<IEnumerable<PushReplacementUser>> SendingPush(ReplacementDtoGet replacementDtoGet);
}