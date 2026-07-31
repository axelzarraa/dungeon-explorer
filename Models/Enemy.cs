namespace DungeonExplorer.Models;

public class Enemy : Entity
{
    public Enemy(string name, Position startPosition, int maxHealth, int attackPower)
        : base(name, startPosition, maxHealth, attackPower) { }

    public override char Symbol => 'E';
}