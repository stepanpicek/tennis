using Tennis.Dto.Requests;
using Tennis.Dto.Responses;

namespace Tennis.Services;

public interface IMatchService
{
    Task StartMatchAsync(StartMatchRequest request);
    Task<MatchProgressResponse> GetMatchProgressAsync(string name);
}