using Tennis.Entities;

namespace Tennis.Repositories;

public interface IMatchRepository
{
    Task<Match> GetMatchAsync(string name);
    Task<Match> GetMatchAsync(Guid id);
    Task SaveMatchAsync(Match match);
    Task<bool> IsMatchExistAsync(string name);
}