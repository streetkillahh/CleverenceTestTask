class Program
{
    static void Main(string[] args)
    {
        // 1-я задача
        string original = "aaabbcccdde";
        Console.WriteLine($"Исходная строка: {original}");

        // Компрессия
        string compressed = StringCompression.Compress(original);
        Console.WriteLine($"Сжатая строка: {compressed}");

        // Декомпрессия
        string decompressed = StringCompression.Decompress(compressed);
        Console.WriteLine($"Восстановленная строка: {decompressed}");

        // 2-я задача
        var tasks = new List<Task>();

        for (int i = 0; i < 10; i++)
        {
            tasks.Add(Task.Run(() => Console.WriteLine($"Read: {Server.GetCount()}")));
            tasks.Add(Task.Run(() => Server.AddToCount(1)));
        }

        Task.WaitAll(tasks.ToArray());

        Console.WriteLine($"Final Count: {Server.GetCount()}");

        // 3-я задача

    }
}