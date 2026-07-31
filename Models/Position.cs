namespace DungeonExplorer.Models;

// 'record struct' = tipe VALUE (bukan reference) yang immutable & auto-generate
// Equals(), GetHashCode(), ToString(). Cocok banget buat koordinat.
// Bandingkan sama Python: mirip @dataclass(frozen=True), atau di Kotlin: data class.
public readonly record struct Position(int X, int Y)
{
    // Expression-bodied method: shorthand untuk method yang isinya 1 ekspresi.
    // Balikin Position BARU (bukan ubah yang lama) karena readonly/immutable.
    public Position Move(int dx, int dy) => new(X + dx, Y + dy);
}