using Tennis.Entities;

namespace Tennis.Repositories;

public class MatchRepository : IMatchRepository
{
    public Task<Match> GetMatchAsync(string name)
    {
        throw new NotImplementedException();
    }

    public Task<Match> GetMatchAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public Task SaveMatchAsync(Match match)
    {
        throw new NotImplementedException();
    }

    public Task<bool> IsMatchExistAsync(string name)
    {
        throw new NotImplementedException();
    }
}