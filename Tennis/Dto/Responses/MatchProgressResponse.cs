using Tennis.Dto.Common;

namespace Tennis.Dto.Responses;

public class MatchProgressResponse
{
    public string Name { get; set; }
    public MatchStatus Status { get; set; }
    public string Score { get; set; }
}