using Tennis.Dto.Common;

namespace Tennis.Entities;

public class Set : Entity
{
    private readonly List<Game> _games = new();

    private Set(Guid id, int setNumber) : base(id)
    {
        SetNumber = setNumber;
    }

    public bool IsSetFinished => Games.All(g => g.IsGameFinished) && 
                                 (Games.Count(g => g.PlayerOneWins) == 6 || Games.Count(g => !g.PlayerOneWins) == 6);

    public int SetNumber { get; init; }
    
    public IReadOnlyCollection<Game> Games => _games;
    
    public static Set Create(int setNumber)
    {
        return new Set(Guid.NewGuid(), setNumber);
    }

    public Game CreateNewGame()
    {
        var newGame = Game.Create(Games.Count % 2 == 0 ? PlayerType.PlayerOne : PlayerType.PlayerTwo);
        _games.Add(newGame);
        return newGame;
    }

    public override string ToString()
    {
        return $"{Games.Count(g => g.PlayerOneWins)}:{Games.Count(g => !g.PlayerOneWins)}";
    }
}