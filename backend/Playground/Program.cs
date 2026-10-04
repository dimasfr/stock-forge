var names = new List<string> { "Keyboard", "Mouse", "Monitor" };

var query = names.Where(n =>
{
    Console.WriteLine($"  cek: {n}");
    return n.StartsWith("M");
});

Console.WriteLine("Query dibuat");
var result = query.ToList();
Console.WriteLine($"Hasil: {string.Join(", ", result)}");