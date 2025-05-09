class Program
{
    static void Main(string[] args)
    {
        // 1-я задача
        Console.WriteLine("Task 1");
        string original = "aaabbcccdde";
        Console.WriteLine($"Исходная строка: {original}");

        // Компрессия
        string compressed = StringCompression.Compress(original);
        Console.WriteLine($"Сжатая строка: {compressed}");

        // Декомпрессия
        string decompressed = StringCompression.Decompress(compressed);
        Console.WriteLine($"Восстановленная строка: {decompressed}");
        Console.WriteLine();

        // 2-я задача
        Console.WriteLine("Task 2");
        var tasks = new List<Task>();

        for (int i = 0; i < 10; i++)
        {
            tasks.Add(Task.Run(() => Console.WriteLine($"Read: {Server.GetCount()}")));
            tasks.Add(Task.Run(() => Server.AddToCount(1)));
        }

        Task.WaitAll(tasks.ToArray());

        Console.WriteLine($"Final Count: {Server.GetCount()}");
        Console.WriteLine();

        // 3-я задача
        Console.WriteLine("Task 3");
        string inputPath = "input.txt";
        string outputPath = "output.txt";
        string problemsPath = "problems.txt";

        var processor = new LogProcessor();
        processor.ProcessLogs(inputPath, outputPath, problemsPath);

        Console.WriteLine("Обработка завершена.");
        Console.WriteLine($"Файл с результатами: {Path.GetFullPath(outputPath)}");
        Console.WriteLine($"Файл с ошибками:     {Path.GetFullPath(problemsPath)}");
    }
}