using Tennis.Dto.Common;

namespace Tennis.Entities;

public class Game : Entity
{
    private Game(Guid id, PlayerType servingPlayer) : base(id)
    {
        ServingPlayer = servingPlayer;
    }

    public GamePoint PlayerOnePoint { get; private set; } = GamePoint.Love;
    
    public GamePoint PlayerTwoPoint { get; private set; } = GamePoint.Love;
    
    public PlayerType ServingPlayer { get; init; }
    
    public bool IsGameFinished => PlayerOnePoint == GamePoint.Game || PlayerTwoPoint == GamePoint.Game;
    
    public bool PlayerOneWins => PlayerOnePoint == GamePoint.Game;
    
    public static Game Create(PlayerType servingPlayer)
    {
        return new Game(Guid.NewGuid(), servingPlayer);
    }
    
    public void PlayerOneScores()
    {
        if(PlayerTwoPoint == GamePoint.Advantage && PlayerOnePoint == GamePoint.Forty)
        {
            PlayerTwoPoint = GamePoint.Forty;
            return;
        }
        
        PlayerOnePoint = GetGamePoint(PlayerOnePoint);
    }

    public void PlayerTwoScores()
    {
        if(PlayerOnePoint == GamePoint.Advantage && PlayerTwoPoint == GamePoint.Forty)
        {
            PlayerOnePoint = GamePoint.Forty;
            return;
        }
        
        PlayerTwoPoint = GetGamePoint(PlayerTwoPoint);
    }
    
    private GamePoint GetGamePoint(GamePoint actualGamePoint)
    {
        if (IsGameFinished)
        {
            throw new InvalidOperationException($"Game {Id} is already finished");
        }
        
        if (PlayerOnePoint == GamePoint.Forty && PlayerTwoPoint == GamePoint.Forty)
        {
            return GamePoint.Advantage;
        }

        if (actualGamePoint is GamePoint.Advantage or GamePoint.Forty)
        {
            return GamePoint.Game;
        }
        
        return ++actualGamePoint;
    }
}