using MassTransit;
using Tennis.Dto.Events;

namespace Tennis.Services;

public class GameConsumer : IConsumer<PlayGameEvent>
{
    private readonly IGameService _gameService;

    public GameConsumer(IGameService gameService)
    {
        _gameService = gameService;
    }

    public async Task Consume(ConsumeContext<PlayGameEvent> context)
    {
        var playGameEvent = context.Message;
        await _gameService.PlayGameAsync(playGameEvent);
    }
}