using MassTransit;
using Tennis.Dto.Common;
using Tennis.Dto.Events;
using Tennis.Dto.Requests;
using Tennis.Dto.Responses;
using Tennis.Entities;
using Tennis.Repositories;

namespace Tennis.Services;

public class MatchService : IMatchService
{
    private readonly IBus _bus;
    private readonly IMatchRepository _matchRepository;

    public MatchService(IBus bus, IMatchRepository matchRepository)
    {
        _bus = bus;
        _matchRepository = matchRepository;
    }

    public async Task StartMatchAsync(StartMatchRequest request)
    {
        if (await _matchRepository.IsMatchExistAsync(request.Name))
        {
            throw new InvalidOperationException("Match already exists");
        }
        
        var newMatch = Match.Create(request.Name, Player.Create(request.FirstPlayerExperience), Player.Create(request.SecondPlayerExperience));
        var newSet = newMatch.CreateNewSet();
        var newGame = newSet.CreateNewGame();
        
        await _matchRepository.SaveMatchAsync(newMatch);

        await _bus.Publish(new PlayGameEvent
        {
            MatchId = newMatch.Id,
            GameId = newGame.Id,
            ActualPlayer = newGame.ServingPlayer
        });
    }

    public async Task<MatchProgressResponse> GetMatchProgressAsync(string name)
    {
        // Get match from database
        var match = await _matchRepository.GetMatchAsync(name);
        if(match == null)
        {
            throw new InvalidOperationException("Match not found");
        }
        
        return new MatchProgressResponse
        {
            Name = name,
            Status = match.IsMatchFinished ? MatchStatus.Finished : MatchStatus.Running,
            Score = string.Join(", ", match.Sets)
        };
    }
}