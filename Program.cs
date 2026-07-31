using DungeonExplorer.Models;

const int MapWidth = 10;
const int MapHeight = 6;

var player = new Player("Hero", new Position(0, 0));

var enemies = new List<Enemy>
{
    new("Goblin", new Position(5, 2), maxHealth: 30, attackPower: 8),
    new("Orc", new Position(8, 4), maxHealth: 50, attackPower: 12)
};

// List of tuple (Position, IItem) buat item yang tersebar di map.
// Tuple dengan nama field bikin 'g.Position' / 'g.Item' bisa dipanggil langsung.
var groundItems = new List<(Position Position, IItem Item)>
{
    (new Position(2, 1), new Potion("Ramuan Kecil", healAmount: 20)),
    (new Position(7, 1), new Weapon("Pedang Karat", attackBonus: 5)),
    (new Position(3, 4), new Potion("Ramuan Besar", healAmount: 40))
};

player.Damaged += (entity, amount) =>
    Console.WriteLine($">> {entity.Name} kena damage {amount}! HP tersisa: {entity.Health}/{entity.MaxHealth}");
player.Died += entity => Console.WriteLine($">> {entity.Name} telah gugur...");

foreach (var enemy in enemies)
{
    enemy.Damaged += (entity, amount) =>
        Console.WriteLine($">> {entity.Name} kena damage {amount}! HP tersisa: {entity.Health}/{entity.MaxHealth}");
    enemy.Died += entity => Console.WriteLine($">> {entity.Name} dikalahkan!");
}

var state = GameState.Playing;

Console.WriteLine("=== Dungeon Explorer ===");
Console.WriteLine("W A S D = gerak, I = lihat tas, U = pakai item, Q = keluar.\n");

while (state == GameState.Playing)
{
    RenderMap();
    Console.Write("Perintah: ");
    var input = Console.ReadLine()?.Trim().ToUpperInvariant();

    if (input == "Q")
    {
        Console.WriteLine("Sampai jumpa, penjelajah!");
        break;
    }

    if (input == "I")
    {
        ShowInventory();
        continue; // Cuma lihat tas, nggak makan giliran.
    }

    if (input == "U")
    {
        UseItem();
        continue;
    }

    var (dx, dy) = input switch
    {
        "W" => (0, -1),
        "S" => (0, 1),
        "A" => (-1, 0),
        "D" => (1, 0),
        _ => (0, 0)
    };

    var newPos = player.Position.Move(dx, dy);
    bool outOfBounds = newPos.X < 0 || newPos.X >= MapWidth || newPos.Y < 0 || newPos.Y >= MapHeight;
    var targetEnemy = enemies.FirstOrDefault(e => e.Position == newPos && e.IsAlive);

    if (outOfBounds)
    {
        Console.WriteLine("Kamu menabrak dinding!");
    }
    else if (targetEnemy is not null)
    {
        Console.WriteLine($"\n--- Bertarung melawan {targetEnemy.Name} ---");
        targetEnemy.TakeDamage(player.AttackPower);

        if (targetEnemy.IsAlive)
        {
            player.TakeDamage(targetEnemy.AttackPower);
        }
    }
    else
    {
        player.MoveTo(newPos);
        TryPickUpItem(newPos);
    }

    if (!player.IsAlive)
    {
        state = GameState.Defeat;
    }
    else if (enemies.All(e => !e.IsAlive))
    {
        state = GameState.Victory;
    }
}

if (state == GameState.Victory)
{
    Console.WriteLine("\n🎉 Semua musuh berhasil dikalahkan! Kamu menang!");
}
else if (state == GameState.Defeat)
{
    Console.WriteLine("\n💀 Kamu kalah... Game Over.");
}

void TryPickUpItem(Position pos)
{
    var index = groundItems.FindIndex(g => g.Position == pos);
    if (index == -1) return;

    var picked = groundItems[index];
    player.Inventory.Add(picked.Item);
    groundItems.RemoveAt(index);
    Console.WriteLine($">> Kamu memungut {picked.Item.Name}!");
}

void ShowInventory()
{
    Console.WriteLine("\n--- Tas Kamu ---");
    if (player.Inventory.Count == 0)
    {
        Console.WriteLine("(kosong)");
    }
    else
    {
        // LINQ 'Select' dengan index buat nomorin item pas ditampilin.
        foreach (var (item, i) in player.Inventory.Items.Select((it, idx) => (it, idx)))
        {
            Console.WriteLine($"{i + 1}. {item.Name} [{item.Symbol}]");
        }
    }
    Console.WriteLine("Tekan Enter buat lanjut...");
    Console.ReadLine();
}

void UseItem()
{
    if (player.Inventory.Count == 0)
    {
        Console.WriteLine("Tas kamu kosong!");
        return;
    }

    ShowInventory();
    Console.Write("Ketik nama item yang mau dipakai: ");
    var itemName = Console.ReadLine()?.Trim();

    var item = player.Inventory.FindByName(itemName ?? "");
    if (item is null)
    {
        Console.WriteLine("Item nggak ditemukan di tas kamu.");
        return;
    }

    var message = item.Use(player);
    Console.WriteLine($">> {message}");
    player.Inventory.Remove(item);
}

void RenderMap()
{
    Console.Clear();
    for (int y = 0; y < MapHeight; y++)
    {
        for (int x = 0; x < MapWidth; x++)
        {
            var pos = new Position(x, y);

            if (player.Position == pos)
            {
                Console.Write(player.Symbol);
                continue;
            }

            var enemyHere = enemies.FirstOrDefault(e => e.Position == pos && e.IsAlive);
            if (enemyHere is not null)
            {
                Console.Write(enemyHere.Symbol);
                continue;
            }

            var itemHere = groundItems.FirstOrDefault(g => g.Position == pos);
            Console.Write(itemHere.Item is not null ? itemHere.Item.Symbol : '.');
        }
        Console.WriteLine();
    }

    Console.WriteLine($"\nHP {player.Name}: {player.Health}/{player.MaxHealth}  |  ATK: {player.AttackPower}  |  Item di tas: {player.Inventory.Count}");
    Console.WriteLine($"Musuh tersisa: {enemies.Count(e => e.IsAlive)}\n");
}