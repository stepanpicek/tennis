using Tennis.Dto.Common;

namespace Tennis.Dto.Events;

public class PlayGameEvent
{
    public Guid MatchId { get; set; }
    public Guid GameId { get; set; }
    public PlayerType ActualPlayer { get; set; }
}