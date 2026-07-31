namespace DungeonExplorer.Models;

// 'enum' = daftar nilai tetap yang punya nama jelas. Jauh lebih aman & jelas
// daripada pakai angka mentah (0, 1, 2) atau string sembarangan ("playing", "win")
// yang gampang typo dan nggak di-cek compiler.
public enum GameState
{
    Playing,
    Victory,
    Defeat
}