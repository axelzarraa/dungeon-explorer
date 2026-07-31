namespace DungeonExplorer.Models;

// ': IItem' di sini artinya IMPLEMENT interface, beda dari ': Entity' yang
// artinya INHERIT/mewarisi. Cara nulisnya sama, tapi maknanya beda.
public class Potion : IItem
{
    public string Name { get; }
    public char Symbol => '!';
    public int HealAmount { get; }

    public Potion(string name, int healAmount)
    {
        Name = name;
        HealAmount = healAmount;
    }

    public string Use(Player player)
    {
        player.Heal(HealAmount);
        return $"{player.Name} minum {Name}, pulih {HealAmount} HP!";
    }
}