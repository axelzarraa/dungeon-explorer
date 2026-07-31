namespace DungeonExplorer.Models;

public abstract class Entity
{
    public string Name { get; }
    public Position Position { get; protected set; }
    public int Health { get; protected set; }
    public int MaxHealth { get; }

    // 'protected set' = cuma Entity & class turunannya yang boleh ubah nilai ini,
    // makanya IncreaseAttackPower() di bawah boleh nulis ke AttackPower.
    public int AttackPower { get; protected set; }
    public abstract char Symbol { get; }

    public event Action<Entity, int>? Damaged;
    public event Action<Entity>? Died;

    protected Entity(string name, Position startPosition, int maxHealth, int attackPower)
    {
        Name = name;
        Position = startPosition;
        MaxHealth = maxHealth;
        Health = maxHealth;
        AttackPower = attackPower;
    }

    public bool IsAlive => Health > 0;
    public void MoveTo(Position newPosition) => Position = newPosition;

    public virtual void TakeDamage(int amount)
    {
        int actualDamage = Math.Min(amount, Health);
        Health -= actualDamage;
        Damaged?.Invoke(this, actualDamage);

        if (!IsAlive)
        {
            Died?.Invoke(this);
        }
    }

    // Dipakai item Potion buat mulihin HP, dibatasi supaya nggak lebih dari MaxHealth.
    public void Heal(int amount) => Health = Math.Min(MaxHealth, Health + amount);

    // Dipakai item Weapon buat nambah kekuatan serang secara permanen.
    public void IncreaseAttackPower(int amount) => AttackPower += amount;
}