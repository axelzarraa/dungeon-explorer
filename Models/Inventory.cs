namespace DungeonExplorer.Models;

// 'class Inventory<T>' = GENERIC class. 'T' itu placeholder tipe yang baru
// ditentukan pas dipakai (nanti kita pakai 'Inventory<IItem>' di Player).
// Compiler otomatis bikin versi type-safe-nya, sama persis cara kerja List<T>
// bawaan .NET yang udah kamu pakai dari Tahap 2.
//
// 'where T : IItem' = CONSTRAINT (batasan). Artinya T WAJIB implement IItem.
// Nggak bisa nulis 'Inventory<int>' misalnya, karena int nggak implement IItem —
// compiler bakal langsung nolak sebelum program dijalankan.
public class Inventory<T> where T : IItem
{
    private readonly List<T> _items = new();

    // 'IReadOnlyList<T>' = dikasih ke luar cuma buat DIBACA, nggak bisa
    // di-Add/Remove langsung dari luar class ini. Enkapsulasi yang lebih ketat
    // daripada cuma ngasih 'List<T>' mentah-mentah.
    public IReadOnlyList<T> Items => _items;

    public void Add(T item) => _items.Add(item);
    public bool Remove(T item) => _items.Remove(item);
    public int Count => _items.Count;

    // LINQ: cari item pertama yang namanya cocok, case-insensitive.
    // 'T?' = boleh null kalau nggak ketemu.
    public T? FindByName(string name) =>
        _items.FirstOrDefault(i => i.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
}