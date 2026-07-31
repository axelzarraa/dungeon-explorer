namespace DungeonExplorer.Models;

public class Weapon : IItem
{
    public string Name { get; }
    public char Symbol => '/';
    public int AttackBonus { get; }

    public Weapon(string name, int attackBonus)
    {
        Name = name;
        AttackBonus = attackBonus;
    }

    public string Use(Player player)
    {
        player.IncreaseAttackPower(AttackBonus);
        return $"{player.Name} melengkapi {Name}! Attack Power naik jadi {player.AttackPower}.";
    }
}