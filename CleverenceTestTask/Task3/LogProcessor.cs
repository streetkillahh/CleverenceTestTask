using System;
using System.Globalization;
using System.IO;
using System.Text.RegularExpressions;

public class LogProcessor
{
    public void ProcessLogs(string inputPath, string outputPath, string problemsPath)
    {
        using var reader = new StreamReader(inputPath);
        using var writer = new StreamWriter(outputPath);
        using var problemWriter = new StreamWriter(problemsPath);

        string? line;
        while ((line = reader.ReadLine()) != null)
        {
            if (TryParseFormat1(line, out var output) || TryParseFormat2(line, out output))
            {
                writer.WriteLine(output);
            }
            else
            {
                problemWriter.WriteLine(line);
            }
        }
    }

    private bool TryParseFormat1(string line, out string output)
    {
        output = string.Empty;
        var regex = new Regex(@"^(\d{2}\.\d{2}\.\d{4}) (\d{2}:\d{2}:\d{2}\.\d{3}) (\w+)\s+(.*)$");

        var match = regex.Match(line);
        if (!match.Success) return false;

        try
        {
            string dateStr = match.Groups[1].Value;
            string time = match.Groups[2].Value;
            string level = NormalizeLevel(match.Groups[3].Value);
            string message = match.Groups[4].Value;

            var date = DateTime.ParseExact(dateStr, "dd.MM.yyyy", CultureInfo.InvariantCulture);
            output = $"{date:yyyy-MM-dd}\t{time}\t{level}\tDEFAULT\t{message}";
            return true;
        }
        catch
        {
            return false;
        }
    }

    private bool TryParseFormat2(string line, out string output)
    {
        output = string.Empty;
        var regex = new Regex(@"^(\d{4}-\d{2}-\d{2}) (\d{2}:\d{2}:\d{2}\.\d+)\|\s*(\w+)\|\d+\|([^|]+)\|\s*(.*)$");

        var match = regex.Match(line);
        if (!match.Success) return false;

        try
        {
            string dateStr = match.Groups[1].Value;
            string time = match.Groups[2].Value;
            string level = NormalizeLevel(match.Groups[3].Value);
            string method = match.Groups[4].Value.Trim();
            string message = match.Groups[5].Value;

            var date = DateTime.ParseExact(dateStr, "yyyy-MM-dd", CultureInfo.InvariantCulture);
            output = $"{date:yyyy-MM-dd}\t{time}\t{level}\t{method}\t{message}";
            return true;
        }
        catch
        {
            return false;
        }
    }

    private string NormalizeLevel(string level)
    {
        return level.ToUpper() switch
        {
            "INFORMATION" => "INFO",
            "INFO" => "INFO",
            "WARNING" => "WARN",
            "WARN" => "WARN",
            "ERROR" => "ERROR",
            "DEBUG" => "DEBUG",
            _ => "INFO" // по умолчанию
        };
    }
}
