# Dungeon Explorer

Console game 2D berbasis grid, dibuat sambil belajar fundamental C# — OOP, inheritance, interface, generics, LINQ, event/delegate, dan enum.

## Fitur

- Pergerakan pemain di grid ASCII (W/A/S/D)
- Sistem combat (bump attack) melawan musuh
- Item & inventory (Potion untuk heal, Weapon untuk nambah attack power)
- Kondisi menang/kalah

## Cara menjalankan

Butuh [.NET SDK](https://dotnet.microsoft.com/download) (versi 10 direkomendasikan).

```bash
dotnet run
```

## Kontrol

| Tombol | Aksi |
|---|---|
| `W` `A` `S` `D` | Gerak (atau menyerang kalau ada musuh di arah itu) |
| `I` | Lihat isi tas |
| `U` | Pakai item dari tas |
| `Q` | Keluar |

## Struktur Project

```
DungeonExplorer/
├── DungeonExplorer.csproj
├── Program.cs              # Entry point & game loop
└── Models/
    ├── Position.cs          # record struct - koordinat (X, Y)
    ├── Entity.cs             # abstract class - basis Player & Enemy
    ├── Player.cs             # class - karakter yang dikontrol pemain
    ├── Enemy.cs               # class - musuh
    ├── GameState.cs           # enum - status permainan
    ├── IItem.cs                # interface - kontrak untuk item
    ├── Potion.cs                # class - item penyembuh
    ├── Weapon.cs                 # class - item penambah serangan
    └── Inventory.cs               # generic class - penyimpanan item
```

## Konsep C# yang dipelajari

- OOP: class, property, constructor, access modifier
- Inheritance & polymorphism (`abstract class Entity`)
- Interface (`IItem`)
- Generics (`Inventory<T> where T : IItem`)
- LINQ (`FirstOrDefault`, `Select`, `Count`, `All`)
- Event & delegate (`Action<T>`, `event`)
- Enum (`GameState`)