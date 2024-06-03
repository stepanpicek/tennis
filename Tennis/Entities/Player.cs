namespace Tennis.Entities;

public sealed class Player : Entity
{
    private Player(Guid id, int experience) : base(id)
    {
        Experience = experience;
    }
    
    public int Experience { get; private set; }

    public static Player Create(int experience)
    {
        var player = new Player(Guid.NewGuid(), experience);
        return player;
    }
}