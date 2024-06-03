using Tennis.Dto.Common;

namespace Tennis.Extensions;

public static class MatchExtensions
{
    public static PlayerType GetSecondPlayer(this PlayerType playerType)
    {
        return playerType == PlayerType.PlayerOne ? PlayerType.PlayerTwo : PlayerType.PlayerOne;
    }
}