namespace DungeonExplorer.Models;

// 'interface' = KONTRAK murni. Siapapun yang 'implement' interface ini WAJIB
// punya semua anggota yang didaftarkan (Name, Symbol, Use), tanpa perlu
// hubungan kekeluargaan (inheritance) sama sekali.
//
// Bedanya sama abstract class Entity yang kita pakai di Tahap 2:
// - Potion dan Weapon SAMA SEKALI NGGAK berhubungan (bukan turunan satu sama lain),
//   tapi keduanya bisa "berjanji" mengikuti kontrak IItem yang sama.
// - Satu class boleh implement BANYAK interface sekaligus (beda dari inheritance
//   yang cuma boleh 1 base class).
public interface IItem
{
    string Name { get; }
    char Symbol { get; }

    // Method ini WAJIB diisi tiap class yang implement IItem, isinya beda-beda
    // tergantung jenis itemnya (potion = heal, weapon = nambah attack).
    string Use(Player player);
}