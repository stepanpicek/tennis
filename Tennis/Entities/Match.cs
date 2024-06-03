namespace Tennis.Entities;

public sealed class Match : Entity
{
    private readonly List<Set> _sets = new();

    private Match(Guid id, string name, Player playerOne, Player playerTwo) : base(id)
    {
        PlayerOne = playerOne;
        PlayerTwo = playerTwo;
        Name = name;
    }

    public string Name { get; init; }

    public Player PlayerOne { get; init; }

    public Player PlayerTwo { get; set; }

    public IReadOnlyCollection<Set> Sets => _sets;

    public bool IsMatchFinished => Sets.Count(s => s.IsSetFinished) == 3;

    public static Match Create(string name, Player playerOne, Player playerTwo)
    {
        return new Match(Guid.NewGuid(), name, playerOne, playerTwo);
    }

    public Set CreateNewSet()
    {
        var set = Set.Create(Sets.Count+1);
        _sets.Add(set);
        return set;
    }
}