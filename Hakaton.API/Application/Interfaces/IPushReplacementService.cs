using Hakaton.API.Domen.Entities;

public interface IPushReplacementService
{
    public Task<IEnumerable<PushReplacementUser>> SendingPush(Replacement replacement);
}