using Tennis.Dto.Events;

namespace Tennis.Services;

public interface IGameService
{
    Task PlayGameAsync(PlayGameEvent playGameEvent);
}