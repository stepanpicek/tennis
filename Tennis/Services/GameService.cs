using MassTransit;
using Tennis.Dto.Common;
using Tennis.Dto.Events;
using Tennis.Entities;
using Tennis.Extensions;
using Tennis.Repositories;

namespace Tennis.Services;

public class GameService : IGameService
{
    private readonly IMatchRepository _matchRepository;
    private readonly IBus _bus;
    private readonly Random _random = new();

    public GameService(IMatchRepository matchRepository, IBus bus)
    {
        _matchRepository = matchRepository;
        _bus = bus;
    }

    public async Task PlayGameAsync(PlayGameEvent playGameEvent)
    {
        var match = await _matchRepository.GetMatchAsync(playGameEvent.MatchId);
        if (match == null)
        {
            throw new InvalidOperationException("Match not found");
        }

        var game = match.Sets
            .SelectMany(s => s.Games)
            .SingleOrDefault(g => g.Id == playGameEvent.GameId);
        
        if(game == null)
        {
            throw new InvalidOperationException("Game not found");
        }
        
        var player = playGameEvent.ActualPlayer == PlayerType.PlayerOne ? match.PlayerOne : match.PlayerTwo;
        var probability = _random.Next(0, 100);
        if (probability <= player.Experience)
        {
            await _bus.Publish(new PlayGameEvent
            {
                MatchId = playGameEvent.MatchId,
                GameId = playGameEvent.GameId,
                ActualPlayer = playGameEvent.ActualPlayer.GetSecondPlayer()
            });
            return;
        }

        await LooseGameAsync(playGameEvent, match);
    } 
    
    private async Task LooseGameAsync(PlayGameEvent playGameEvent, Match game)
    {
        // TODO: Logic if player loose the game
    }
}