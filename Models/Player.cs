namespace DungeonExplorer.Models;

public class Player : Entity
{
    // 'Inventory<IItem>' = generic Inventory yang kita bikin tadi, di-set
    // konkretnya buat nyimpen tipe IItem (jadi bisa isi Potion, Weapon, dll).
    public Inventory<IItem> Inventory { get; } = new();

    public Player(string name, Position startPosition, int maxHealth = 100, int attackPower = 15)
        : base(name, startPosition, maxHealth, attackPower) { }

    public override char Symbol => '@';
}